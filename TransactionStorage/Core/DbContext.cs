using TransactionStorage.Interface;
using TransactionStorage.Model;

namespace TransactionStorage.Core
{
    public class DbContext : IDbContext<Transaction>
    {
        private readonly HashSet<Transaction> _storage;

        public DbContext(IEqualityComparer<Transaction> comparer)
        {
            _storage = new HashSet<Transaction>(comparer);
        }

        public bool Exists(int id)
        {
            return _storage.TryGetValue(new Transaction() { Id = id }, out _);
        }

        public Transaction? Get(int id)
        {
            _ = _storage.TryGetValue(new Transaction() { Id = id }, out var result);
            return result;
        }

        public bool Add(Transaction entry)
        {
            return _storage.Add(entry);
        }
    }
}
