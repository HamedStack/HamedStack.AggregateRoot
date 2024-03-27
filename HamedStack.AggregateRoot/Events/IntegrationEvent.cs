using System.Text.Json.Serialization;

namespace HamedStack.TheAggregateRoot.Events;

// No need to implement any interface.
// It works with EventBus not MediatR.
public class IntegrationEvent
{
    [JsonConstructor]
    protected IntegrationEvent()
    {

    }
    public Guid CorrelationId => Guid.NewGuid();
    public string EventType => GetType().Name;
    public string EventKey => GetType().AssemblyQualifiedName!;
    public DateTimeOffset DateOccurred => DateTimeOffset.UtcNow;
}