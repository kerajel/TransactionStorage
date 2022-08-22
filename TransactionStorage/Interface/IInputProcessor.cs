namespace TransactionStorage.Interface
{
    public interface IInputProcessor
    {
        bool CanHandle(string? userInput);

        void Process();
    }
}
