using TransactionStorage.Interface;
using TransactionStorage.Model;

namespace TransactionStorage.Core
{
    public class InputProvider : IInputProvider
    {
        private readonly IInputEvaluator _inputEvaluator;

        public InputProvider(IInputEvaluator inputEvaluator)
        {
            _inputEvaluator = inputEvaluator;
        }

        public UserInput GetUserInput()
        {
            var userInput = Console.ReadLine();
            var userCommand = _inputEvaluator.EvaluateInput(userInput);
            return new UserInput(userInput, userCommand);
        }
    }
}
