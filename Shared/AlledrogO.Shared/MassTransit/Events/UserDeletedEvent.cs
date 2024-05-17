namespace AlledrogO.Shared.MassTransit.Events;

public record UserDeletedEvent()
{
    public Guid UserId { get; init; }
}