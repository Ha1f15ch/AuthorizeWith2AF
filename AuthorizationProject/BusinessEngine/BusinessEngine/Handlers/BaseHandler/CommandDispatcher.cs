using Microsoft.Extensions.DependencyInjection;

namespace BusinessEngine.Handlers.BaseHandler;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider; // это root-провайдер

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> SendAsync<TCommand, TResult>(TCommand command) where TCommand : class
    {
        // не может давать scoped-сервисы
        using var scope = _serviceProvider.CreateScope(); // Создаетсяновуа область (scope) для получения scoped-зависимостей
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(typeof(TCommand), typeof(TResult));
        
        dynamic handler = scope.ServiceProvider.GetRequiredService(handlerType);
        return (TResult)await handler.HandleAsync(command);
    }
}