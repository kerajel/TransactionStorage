using TransactionStorage.Model;

namespace TransactionStorage.Interface
{
    public interface IDbContext<T> where T : class, new()
    {
        bool Add(T entry);

        bool Exists(int id);

        T? Get(int id);
    }
}