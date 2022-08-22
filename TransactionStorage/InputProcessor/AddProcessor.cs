using Microsoft.Extensions.Logging;
using TransactionStorage.Enum;
using TransactionStorage.Interface;
using TransactionStorage.Model;

namespace TransactionStorage.InputProcessor
{
    public class AddProcessor : IInputProcessor
    {
        private readonly IDbContext<Transaction> _dbContext;
        private readonly IInputProvider _inputProvider;
        private readonly IInputValidator _inputValidator;
        private readonly ILogger<AddProcessor> _logger;
        private readonly AppSettings _appSettings;

        public AddProcessor(
            IDbContext<Transaction> dbContext,
            IInputProvider inputProvider,
            IInputValidator inputValidator,
            ILogger<AddProcessor> logger,
            AppSettings appSettings)
        {
            _dbContext = dbContext;
            _inputProvider = inputProvider;
            _inputValidator = inputValidator;
            _logger = logger;
            _appSettings = appSettings;
        }

        private LinkedList<(int StepId, string Message)> GetProcessSteps()
        {
            (int StepId, string Message)[] steps = new[]
{
                (0, "Введите Id:"),
                (1, $"Введите дату в формате '{_appSettings.DateTimeFormat}':") ,
                (2, "Введите сумму:")
            };

            return new LinkedList<(int StepId, string Message)>(steps);
        }

        public bool CanHandle(string? userInput) => userInput != null && userInput.Equals("Add", StringComparison.OrdinalIgnoreCase);

        public void Process()
        {
            _logger.LogInformation("Добавление новой транзакции (используйте команду Back для редактирования предыдущего свойства или Exit для возврата в предыдущее меню)");

            var transaction = new Transaction();

            var steps = GetProcessSteps();

            var currentStep = steps.First;

            while (currentStep != null)
            {
                _logger.LogInformation(currentStep.Value.Message);

                var userInput = _inputProvider.GetUserInput();

                if (userInput.Command == InputCommand.Exit)
                {
                    return;
                }

                if (userInput.Command == InputCommand.Back)
                {
                    currentStep = currentStep.Previous ?? steps.First;
                    continue;
                }

                var moveToNextStep = currentStep.Value.StepId switch
                {
                    0 => ProcessId(userInput.Value, transaction),
                    1 => ProcessDate(userInput.Value, transaction),
                    2 => ProcessAmount(userInput.Value, transaction),
                    _ => false,
                };

                if (moveToNextStep)
                {
                    currentStep = currentStep.Next;
                }
            }

            SubmitTransaction(transaction);
        }

        private bool ProcessId(string? userInput, Transaction transaction)
        {
            if (!_inputValidator.Validate<int>(userInput, out var id))
            {
                _logger.LogError("Введённое значение не является целым положительным числом");
                return false;
            }

            if (_dbContext.Exists(id))
            {
                _logger.LogError($"Транзакция с ID {id} уже существует");
                return false;
            }

            transaction.Id = id;
            return true;
        }

        private bool ProcessDate(string? userInput, Transaction transaction)
        {
            if (!_inputValidator.Validate<DateTime>(userInput, out var date))
            {
                _logger.LogError($"Введённое значение не является датой. Используйте формат '{_appSettings.DateTimeFormat}'");
                return false;
            }

            transaction.TransactionDate = date;
            return true;
        }

        private bool ProcessAmount(string? userInput, Transaction transaction)
        {
            if (!_inputValidator.Validate<decimal>(userInput, out var amount))
            {
                _logger.LogError("Введённое значение не является числом");
                return false;
            }

            transaction.Amount = amount;
            return true;
        }

        private void SubmitTransaction(Transaction transaction)
        {
            var stored = _dbContext.Add(transaction);

            if (stored)
            {
                _logger.LogInformation("[OK]");
            }
            else
            {
                _logger.LogError("Ошибка при сохранении транзакции");
            }
        }
    }
}
