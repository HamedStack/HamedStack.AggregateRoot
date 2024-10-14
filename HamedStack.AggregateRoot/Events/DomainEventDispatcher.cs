using MediatR;

namespace HamedStack.TheAggregateRoot.Events;

/// <summary>
/// A dispatcher for domain events that utilizes the <see cref="IMediator"/> interface 
/// to publish events to their respective handlers.
/// </summary>
public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="DomainEventDispatcher"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance used for publishing domain events.</param>
    public DomainEventDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Dispatches a collection of domain events asynchronously.
    /// </summary>
    /// <param name="domainEvents">An enumerable collection of <see cref="DomainEvent"/> instances to dispatch.</param>
    /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task DispatchEventsAsync(IEnumerable<DomainEvent> domainEvents, CancellationToken cancellationToken = default)
    {
        foreach (var domainEvent in domainEvents)
        {
            await _mediator.Publish(domainEvent, cancellationToken);
        }
    }

    /// <summary>
    /// Dispatches a single domain event asynchronously.
    /// </summary>
    /// <param name="domainEvent">The <see cref="DomainEvent"/> instance to dispatch.</param>
    /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task DispatchEventAsync(DomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        await _mediator.Publish(domainEvent, cancellationToken);
    }

    /// <summary>
    /// Dispatches a collection of domain events asynchronously,
    /// accepting events as a collection of objects.
    /// </summary>
    /// <param name="domainEvents">An enumerable collection of events as <see cref="object"/> to dispatch.</param>
    /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task DispatchEventsAsync(IEnumerable<object> domainEvents, CancellationToken cancellationToken = default)
    {
        foreach (var domainEvent in domainEvents)
        {
            await _mediator.Publish(domainEvent, cancellationToken);
        }
    }

    /// <summary>
    /// Dispatches a single event asynchronously,
    /// accepting the event as an object.
    /// </summary>
    /// <param name="domainEvent">The event as <see cref="object"/> to dispatch.</param>
    /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task DispatchEventAsync(object domainEvent, CancellationToken cancellationToken = default)
    {
        await _mediator.Publish(domainEvent, cancellationToken);
    }
}