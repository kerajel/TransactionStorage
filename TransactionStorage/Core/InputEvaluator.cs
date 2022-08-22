using TransactionStorage.Enum;
using TransactionStorage.Interface;

namespace TransactionStorage.Core
{
    public class InputEvaluator : IInputEvaluator
    {
        public InputCommand EvaluateInput(string? userInput)
        {
            if (userInput == null)
            {
                return InputCommand.CustomInput;
            }
            else if (CaseInsensitiveEquals(userInput, "Exit"))
            {
                return InputCommand.Exit;
            }
            else if (CaseInsensitiveEquals(userInput, "Back"))
            {
                return InputCommand.Back;
            }
            else
            {
                return InputCommand.CustomInput;
            }
        }

        private static bool CaseInsensitiveEquals(string source, string target)
        {
            return source.Equals(target, StringComparison.OrdinalIgnoreCase);
        }
    }
}
