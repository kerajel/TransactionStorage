using TransactionStorage.Interface;

namespace TransactionStorage.InputParser
{
    public class IntegerParser : IInputParser<int>
    {
        public bool TryParse(string? input, out int result)
        {
            return int.TryParse(input, out result) && result > 0 ;
        }
    }
}
