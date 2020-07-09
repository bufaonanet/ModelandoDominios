using PaymentContext.Shared.Command;

namespace PaymentContext.Domain.Commands
{
    public class CommandResult : ICommandResult
    {
        public CommandResult()
        {

        }

        public CommandResult(bool sucesses, string message)
        {
            Sucesses = sucesses;
            Message = message;
        }

        public bool Sucesses { get; set; }
        public string Message { get; set; }
    }
}