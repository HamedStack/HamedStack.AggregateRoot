namespace HamedStack.TheAggregateRoot.Events;

/// <summary>
/// Defines the contract for a domain event dispatcher that publishes domain events 
/// to their respective handlers asynchronously.
/// </summary>
public interface IDomainEventDispatcher
{
    /// <summary>
    /// Dispatches a collection of domain events asynchronously.
    /// </summary>
    /// <param name="domainEvents">An enumerable collection of <see cref="DomainEvent"/> instances to dispatch.</param>
    /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DispatchEventsAsync(IEnumerable<DomainEvent> domainEvents, CancellationToken cancellationToken = default);

    /// <summary>
    /// Dispatches a single domain event asynchronously.
    /// </summary>
    /// <param name="domainEvent">The <see cref="DomainEvent"/> instance to dispatch.</param>
    /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DispatchEventAsync(DomainEvent domainEvent, CancellationToken cancellationToken = default);

    /// <summary>
    /// Dispatches a collection of domain events asynchronously,
    /// accepting events as a collection of objects.
    /// </summary>
    /// <param name="domainEvents">An enumerable collection of events as <see cref="object"/> to dispatch.</param>
    /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DispatchEventsAsync(IEnumerable<object> domainEvents, CancellationToken cancellationToken = default);

    /// <summary>
    /// Dispatches a single event asynchronously,
    /// accepting the event as an object.
    /// </summary>
    /// <param name="domainEvent">The event as <see cref="object"/> to dispatch.</param>
    /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DispatchEventAsync(object domainEvent, CancellationToken cancellationToken = default);
}