using TransactionStorage.Core;
using TransactionStorage.Model;

namespace UnitTests.TransactionComparerTests
{
    public class TransactionComparerTests
    {
        [Test]
        public void Equals_WhenComparing_ComparesOnlyTransactionIds()
        {
            // Assert
            var sut = new TransactionComparer();

            var t1 = new Transaction() { Id = 1, Amount = 200 };
            var t2 = new Transaction() { Id = 1, Amount = 300 };

            // Act
            var result = sut.Equals(t1, t2);

            // Assert
            result.Should().BeTrue();
        }
    }
}
