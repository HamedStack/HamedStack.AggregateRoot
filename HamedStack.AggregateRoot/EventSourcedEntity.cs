using HamedStack.TheAggregateRoot.Events;

namespace HamedStack.TheAggregateRoot;

/// <summary>
/// Represents an abstract base class for an event-sourced entity,
/// providing mechanisms to handle domain events and maintain state across versions.
/// </summary>
/// <typeparam name="TId">The type of the identifier for the entity, which must not be null.</typeparam>
public abstract class EventSourcedEntity<TId> : Entity<TId> where TId : notnull
{
    private readonly List<DomainEvent> _uncommittedChanges = new();

    /// <summary>
    /// Gets or sets the version of the entity, indicating its state version.
    /// </summary>
    public int Version { get; internal set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EventSourcedEntity{TId}"/> class
    /// with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier for the entity.</param>
    protected EventSourcedEntity(TId id) : base(id) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="EventSourcedEntity{TId}"/> class.
    /// </summary>
    protected EventSourcedEntity() { }

    /// <summary>
    /// Retrieves the list of uncommitted changes (domain events) that have occurred
    /// since the last commit.
    /// </summary>
    /// <returns>An enumerable collection of <see cref="DomainEvent"/> representing uncommitted changes.</returns>
    public IEnumerable<DomainEvent> GetUncommittedChanges()
    {
        return _uncommittedChanges;
    }

    /// <summary>
    /// Marks all uncommitted changes as committed, clearing the internal list of changes.
    /// </summary>
    public void MarkChangesAsCommitted()
    {
        _uncommittedChanges.Clear();
    }

    /// <summary>
    /// Applies a change to the entity and marks it as new, updating the version accordingly.
    /// </summary>
    /// <param name="e">The domain event to apply.</param>
    protected void ApplyChange(DomainEvent e)
    {
        e.Version = Version + 1;
        ApplyChange(e, true);
    }

    /// <summary>
    /// Applies a change to the entity, executing the corresponding apply method
    /// and adding the event to the list of uncommitted changes if it is new.
    /// </summary>
    /// <param name="e">The domain event to apply.</param>
    /// <param name="isNew">Indicates whether the change is new and should be added to the uncommitted changes.</param>
    private void ApplyChange(DomainEvent e, bool isNew)
    {
        var applyMethod = GetType().GetMethod("Apply", new[] { e.GetType() });
        applyMethod?.Invoke(this, new object[] { e });
        if (isNew)
        {
            _uncommittedChanges.Add(e);
        }
    }

    /// <summary>
    /// Loads the entity's state from a history of domain events,
    /// applying each event and updating the version.
    /// </summary>
    /// <param name="history">An enumerable collection of <see cref="DomainEvent"/> representing the entity's history.</param>
    public void LoadsFromHistory(IEnumerable<DomainEvent> history)
    {
        foreach (var e in history)
        {
            ApplyChange(e, false);
            Version = e.Version;
        }
    }
}
