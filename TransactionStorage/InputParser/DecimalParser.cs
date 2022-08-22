using TransactionStorage.Interface;

namespace TransactionStorage.InputParser
{
    public class DecimalParser : IInputParser<decimal>
    {
        public bool TryParse(string? input, out decimal result)
        {
            return decimal.TryParse(input, out result);
        }
    }
}
