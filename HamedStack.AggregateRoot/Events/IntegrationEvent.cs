using System.Text.Json.Serialization;

namespace HamedStack.TheAggregateRoot.Events;

/// <summary>
/// Represents an integration event used to communicate between different parts of the system or across systems.
/// Integration events are typically used in distributed architectures for event-driven communication.
/// </summary>
public class IntegrationEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IntegrationEvent"/> class.
    /// This constructor is protected to prevent direct instantiation from outside this class.
    /// </summary>
    [JsonConstructor]
    protected IntegrationEvent()
    {
    }

    /// <summary>
    /// Gets the unique identifier for the correlation of the event. 
    /// A new GUID is generated each time this property is accessed.
    /// </summary>
    public Guid CorrelationId => Guid.NewGuid();

    /// <summary>
    /// Gets the name of the event type, which is the name of the class that implements this event.
    /// </summary>
    public string EventType => GetType().Name;

    /// <summary>
    /// Gets a string representing the fully qualified name of the event type,
    /// which includes the assembly information.
    /// </summary>
    public string EventKey => GetType().AssemblyQualifiedName!;

    /// <summary>
    /// Gets the date and time when the event occurred, in UTC.
    /// This property is set to the current time at the moment of access.
    /// </summary>
    public DateTimeOffset DateOccurred => DateTimeOffset.UtcNow;
}