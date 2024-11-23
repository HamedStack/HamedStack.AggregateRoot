using System.Text.Json.Serialization;
using MediatR;

namespace HamedStack.TheAggregateRoot.Events;

/// <summary>
/// Represents a notification event within the domain-driven design.
/// </summary>
/// <remarks>
/// Notification events are used to communicate domain events across different parts of the application.
/// Implementing this interface allows for a standardized way to define and handle such events,
/// promoting loose coupling and enhancing the modularity of the application.
/// </remarks>
public class DomainEvent : INotification
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DomainEvent"/> class.
    /// This constructor is protected to prevent direct instantiation from outside this class.
    /// </summary>
    [JsonConstructor]
    protected DomainEvent()
    {
    }

    /// <summary>
    /// Gets the unique identifier for the event. 
    /// A new GUID is generated each time this property is accessed.
    /// </summary>
    public Guid EventId => Guid.NewGuid();

    /// <summary>
    /// Gets or sets the version of the event, indicating its order in the sequence of events.
    /// The default value is 0.
    /// </summary>
    public int EventVersion { get; set; } = 0;

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
    public DateTimeOffset EventDateOccurred => DateTimeOffset.UtcNow;
}


