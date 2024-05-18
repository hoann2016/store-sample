namespace BuildingBlocks.Messaging.Events;

public record IntegrationEvent
{
    public Guid Id => Guid.NewGuid();
    public DateTime OccorredOn { get; } = DateTime.UtcNow;
    public string EventType =>GetType().AssemblyQualifiedName;
}
