namespace AlledrogO.Shared.Commands;

public interface ICommandHandler<TCommand> where TCommand : class, ICommand
{
    Task HandleAsync(TCommand command);
}

public interface ICommandHandler<TCommand, TResult> where TCommand : class, ICommand<TResult>
{
    Task<TResult> HandleAsync(TCommand command);
}