using TransactionStorage.Model;

namespace TransactionStorage.Core
{
    public class TransactionComparer : EqualityComparer<Transaction>
    {
        private readonly IEqualityComparer<int> _c = EqualityComparer<int>.Default;

        public override bool Equals(Transaction? l, Transaction? r)
        {
            if (l == null && r == null)
                return true;
            else if (l == null || r == null)
                return false;

            return _c.Equals(l.Id, r.Id);
        }

        public override int GetHashCode(Transaction t)
        {
            return _c.GetHashCode(t.Id);
        }
    }
}
