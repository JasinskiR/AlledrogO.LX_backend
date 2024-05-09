namespace AlledrogO.Shared.Commands;

public interface ICommandHandler<TCommand> where TCommand : class, ICommand
{
    Task HandleAsync(TCommand command);
}