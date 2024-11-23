namespace HamedStack.TheAggregateRoot.Events;

/// <summary>
/// Provides methods for managing domain events associated with an entity.
/// This includes adding, removing, and clearing domain events.
/// </summary>
public interface IDomainEventManager : IDomainEvent
{
    /// <summary>
    /// Adds a domain event to the entity. This method is used to record a new significant business event that
    /// has occurred to the domain. Adding a domain event to this collection does not automatically publish the event;
    /// it merely records that the event has occurred.
    /// </summary>
    /// <param name="notificationEvent">The domain event to add. It should not be null.</param>
    void AddDomainEvent(DomainEvent notificationEvent);

    /// <summary>
    /// Removes a domain event from the entity. This method is used in situations where an event is added to the domain
    /// events collection by mistake or when the conditions causing the event to be recorded are no longer valid.
    /// </summary>
    /// <param name="notificationEvent">The domain event to remove. It should not be null and should be present in the collection of domain events.</param>
    void RemoveDomainEvent(DomainEvent notificationEvent);

    /// <summary>
    /// Clears all domain events from the entity. This method is typically called after the domain events are published to
    /// an external system or handled internally, indicating that they have been processed and are no longer needed.
    /// Clearing domain events helps prevent the same event from being processed multiple times.
    /// </summary>
    void ClearDomainEvents();
}