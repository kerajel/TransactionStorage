using System.Globalization;
using TransactionStorage.Interface;
using TransactionStorage.Model;

namespace TransactionStorage.InputParser
{
    public class DateTimeParser : IInputParser<DateTime>
    {
        private readonly AppSettings _appSettings;

        public DateTimeParser(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        public bool TryParse(string? input, out DateTime result)
        {
            return DateTime.TryParseExact(input, _appSettings.DateTimeFormat, null, DateTimeStyles.AllowWhiteSpaces, out result);
        }
    }
}
