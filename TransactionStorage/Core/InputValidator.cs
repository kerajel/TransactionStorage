using Autofac;
using TransactionStorage.Interface;

namespace TransactionStorage.Core
{
    public class InputValidator : IInputValidator
    {
        private readonly IComponentContext _context;

        public InputValidator(IComponentContext context)
        {
            _context = context;
        }

        public bool Validate<T>(string? userInput, out T result) where T : struct
        {
            var parser = _context.Resolve<IInputParser<T>>();
            return parser.TryParse(userInput, out result);
        }
    }
}
