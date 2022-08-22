namespace TransactionStorage.Interface
{
    public interface IInputParser<T>
    {
        bool TryParse(string? input, out T result);
    }
}

