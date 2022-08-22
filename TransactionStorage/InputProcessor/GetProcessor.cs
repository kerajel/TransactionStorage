using Microsoft.Extensions.Logging;
using TransactionStorage.Enum;
using TransactionStorage.Interface;
using TransactionStorage.Model;

namespace TransactionStorage.InputProcessor
{
    public class GetProcessor : IInputProcessor
    {
        private readonly IDbContext<Transaction> _dbContext;
        private readonly IInputProvider _inputProvider;
        private readonly ILogger<AddProcessor> _logger;
        private readonly IInputValidator _inputValidator;
        private readonly ISerializer _serializer;

        public GetProcessor(
            IDbContext<Transaction> dbContext,
            IInputProvider inputProvider,
            ILogger<AddProcessor> logger,
            IInputValidator inputValidator,
            ISerializer serializer)
        {
            _dbContext = dbContext;
            _inputProvider = inputProvider;
            _logger = logger;
            _inputValidator = inputValidator;
            _serializer = serializer;
        }
        public bool CanHandle(string? userInput) => userInput != null && userInput.Equals("Get", StringComparison.OrdinalIgnoreCase);

        public void Process()
        {
            _logger.LogInformation("Поиск транзакции по Id (используйте команду Exit для возврата в предыдущее меню)");

            while (true)
            {
                _logger.LogInformation("Введите Id:");

                var userInput = _inputProvider.GetUserInput();

                if (userInput.Command == InputCommand.Exit)
                {
                    return;
                }

                if (!_inputValidator.Validate<int>(userInput.Value, out var id))
                {
                    _logger.LogError("Введённое значение не является целым положительным числом");
                    continue;
                }

                var transaction = _dbContext.Get(id);
                if (transaction != null)
                {
                    var output = _serializer.Serialize(transaction);
                    _logger.LogInformation(output);
                }
                else
                {
                    _logger.LogInformation($"Транзакция с ID {id} не найдена");
                }

                return;
            }
        }
    }
}
