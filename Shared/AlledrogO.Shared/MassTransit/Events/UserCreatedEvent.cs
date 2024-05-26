namespace AlledrogO.Shared.MassTransit.Events;

public record UserCreatedEvent()
{
    public Guid UserId { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
}