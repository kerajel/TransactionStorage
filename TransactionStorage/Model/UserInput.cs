using TransactionStorage.Enum;

namespace TransactionStorage.Model
{
    public class UserInput
    {
        public UserInput(string? value, InputCommand command)
        {
            Value = value;
            Command = command;
        }

        public string? Value { get; }

        public InputCommand Command { get; }
    }
}
