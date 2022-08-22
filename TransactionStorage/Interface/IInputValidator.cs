namespace TransactionStorage.Interface
{
    public interface IInputValidator
    {
        bool Validate<T>(string? userInput, out T result) where T : struct;
    }
}
