namespace HamedStack.TheAggregateRoot.Events;

public interface IDomainEventDispatcher
{
    Task DispatchEventsAsync(IEnumerable<DomainEvent> domainEvents, CancellationToken cancellationToken = default);
    Task DispatchEventAsync(DomainEvent domainEvent, CancellationToken cancellationToken = default);
    Task DispatchEventsAsync(IEnumerable<object> domainEvents, CancellationToken cancellationToken = default);
    Task DispatchEventAsync(object domainEvent, CancellationToken cancellationToken = default);
}