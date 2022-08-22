namespace TransactionStorage.Interface
{
    public interface ISerializer
    {
        string Serialize(object o);
    }
}