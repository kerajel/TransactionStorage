using Microsoft.Extensions.Logging;
using Polly;
using TransactionStorage.Enum;
using TransactionStorage.Interface;

namespace TransactionStorage.Core
{
    public class StorageService : IStorageService
    {
        private readonly IEnumerable<IInputProcessor> _inputProcessors;
        private readonly IInputProvider _inputProvider;
        private readonly ILogger<StorageService> _logger;

        public StorageService(
            IEnumerable<IInputProcessor> inputProcessors,
            IInputProvider inputProvider,
            ILogger<StorageService> logger)
        {
            _inputProcessors = inputProcessors;
            _inputProvider = inputProvider;
            _logger = logger;
        }

        public void RunService()
        {
            _logger.LogInformation("Доступные команды:");
            _logger.LogInformation("Add - добавить новую транзакцию");
            _logger.LogInformation("Get - получить существующую транзакцию");
            _logger.LogInformation("Exit - вернуться в предыдущий раздел \\ выйти из приложения");

            var retryPolicy = Policy.Handle<Exception>()
                .RetryForever(onRetry: (exception) =>
                {
                    _logger.LogCritical($"Критическая ошибка");
                    _logger.LogCritical(exception.ToString());

                    _logger.LogInformation("Введите любую команду для продолжения");
                    _ = _inputProvider.GetUserInput();
                });

            retryPolicy.Execute(() =>
            {
                while (true)
                {
                    _logger.LogInformation("=== Основное меню ===");

                    var userInput = _inputProvider.GetUserInput();

                    if (userInput.Command == InputCommand.Exit)
                    {
                        return;
                    }

                    var processor = _inputProcessors.FirstOrDefault(r => r.CanHandle(userInput.Value));

                    if (processor == null)
                    {
                        _logger.LogInformation("Некорректный ввод");
                        continue;
                    }

                    processor.Process();
                }
            });
        }
    }
}