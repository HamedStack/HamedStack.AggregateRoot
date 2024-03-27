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
    [JsonConstructor]
    protected DomainEvent()
    {
        
    }

    public string EventType => GetType().Name;
    public string EventKey => GetType().AssemblyQualifiedName!;
    public DateTimeOffset DateOccurred => DateTimeOffset.UtcNow;
}


