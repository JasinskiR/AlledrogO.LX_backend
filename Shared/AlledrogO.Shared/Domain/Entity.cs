namespace AlledrogO.Shared.Domain;

public abstract class Entity<TId>
{
    public TId Id { get; protected set; }
    
    public uint Version { get; private set;}
    
    public IEnumerable<IDomainEvent> Events => _events;
    
    private readonly List<IDomainEvent> _events = new();

    // adding events should be done only by entity invariants
    protected void AddEvent(IDomainEvent domainEvent) => _events.Add(domainEvent);
    
    // public, useful for tests
    public void ClearEvents() => _events.Clear();

}