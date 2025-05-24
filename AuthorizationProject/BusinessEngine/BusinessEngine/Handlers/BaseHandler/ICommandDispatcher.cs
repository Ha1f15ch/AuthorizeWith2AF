namespace BusinessEngine.Handlers.BaseHandler;

public interface ICommandDispatcher
{
    Task<TResult> SendAsync<TCommand, TResult>(TCommand command) where TCommand : class;
}