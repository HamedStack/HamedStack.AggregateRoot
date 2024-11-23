using System.ComponentModel.DataAnnotations.Schema;

namespace HamedStack.TheAggregateRoot.Events;

/// <summary>
/// Exposes the list of domain events associated with an entity. 
/// This is used for retrieving the recorded domain events.
/// </summary>
public interface IDomainEvent
{
    /// <summary>
    /// Gets the list of domain events associated with the entity. Domain events are used to record all significant
    /// business events that occur to the domain. This list can be used to publish events to the outside world,
    /// such as updating an external system or modifying application state.
    /// </summary>
    [NotMapped]
    IReadOnlyCollection<DomainEvent> DomainEvents { get; }
}