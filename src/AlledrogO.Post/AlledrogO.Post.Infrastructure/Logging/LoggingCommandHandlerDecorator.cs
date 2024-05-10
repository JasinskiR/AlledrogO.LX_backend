using AlledrogO.Shared.Commands;
using Microsoft.Extensions.Logging;

namespace AlledrogO.Post.Infrastructure.Logging;

internal sealed class LoggingCommandHandlerDecorator<TCommand> 
    : ICommandHandler<TCommand> where TCommand : class, ICommand
{
    private readonly ICommandHandler<TCommand> _commandHandler;
    private readonly ILogger<LoggingCommandHandlerDecorator<TCommand>> _logger;

    public LoggingCommandHandlerDecorator(ICommandHandler<TCommand> commandHandler, 
        ILogger<LoggingCommandHandlerDecorator<TCommand>> logger)
    {
        _commandHandler = commandHandler;
        _logger = logger;
    }

    public async Task HandleAsync(TCommand command)
    {
        var commandType = command.GetType().Name;
        
        try
        {
            _logger.LogInformation($"Handling command: {commandType}");
            await _commandHandler.HandleAsync(command);
            _logger.LogInformation($"Command handled: {commandType}");
        }
        catch (Exception)
        {
            _logger.LogError($"Command failed: {commandType}");
            throw;
        }
    }
}