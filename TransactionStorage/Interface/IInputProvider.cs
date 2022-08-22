using TransactionStorage.Model;

namespace TransactionStorage.Interface
{
    public interface IInputProvider
    {
        UserInput GetUserInput();
    }
}
