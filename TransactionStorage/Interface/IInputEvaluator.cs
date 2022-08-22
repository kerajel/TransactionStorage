using TransactionStorage.Enum;

namespace TransactionStorage.Interface
{
    public interface IInputEvaluator
    {
        InputCommand EvaluateInput(string? userInput);
    }
}