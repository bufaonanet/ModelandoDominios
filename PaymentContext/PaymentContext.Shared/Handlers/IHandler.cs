using PaymentContext.Shared.Command;

namespace PaymentContext.Shared.Handlers
{
    public interface IHandler<T> where T : ICommand
    {
        ICommandResult Handler(T command);
    }
}