using System.Windows.Input;

namespace BusinessEngine.Handlers.BaseHandler;

public interface ICommandHandler<TCommand, TResult> where TCommand : class 
{
    Task<TResult> HandleAsync(TCommand command);
}