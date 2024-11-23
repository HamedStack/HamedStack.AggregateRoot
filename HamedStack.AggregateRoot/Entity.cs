using System.ComponentModel.DataAnnotations.Schema;
using HamedStack.TheAggregateRoot.Abstractions;
using HamedStack.TheAggregateRoot.Events;

namespace HamedStack.TheAggregateRoot;

/// <summary>
/// Represents the base entity for domain entities.
/// </summary>
/// <typeparam name="TId">The type of the entity identifier.</typeparam>
public abstract class Entity<TId> : IIdentifier<TId>, IDomainEventManager
    where TId : notnull
{
    private int _requestedHashCode = int.MinValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{T}"/> class with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier for the entity.</param>
    protected Entity(TId id)
    {
        Id = id;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{T}"/> class without setting an identifier.
    /// </summary>
    protected Entity()
    {
        Id = default!;
    }

    /// <summary>
    /// Gets or sets the identifier for the entity.
    /// </summary>
    public TId Id { get; set; }

    private readonly List<DomainEvent> _domainEvents = new();
    /// <summary>
    /// Gets an immutable collection of domain events associated with the entity.
    /// </summary>
    /// <value>
    /// An immutable collection of domain events.
    /// </value>
    /// <remarks>
    /// Use this property to iterate through the domain events without modifying the underlying collection.
    /// </remarks>
    [NotMapped]
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Adds a domain event to the current entity's list of domain events.
    /// </summary>
    /// <param name="newEvent">The domain event to add.</param>
    /// <remarks>
    /// Domain events are typically used in domain-driven design to capture 
    /// side effects of actions on entities that need to be propagated 
    /// outside the domain layer.
    /// </remarks>
    public virtual void AddDomainEvent(DomainEvent newEvent)
    {
        _domainEvents.Add(newEvent);
    }

    /// <summary>
    /// Removes a domain event from the current entity's list of domain events.
    /// </summary>
    /// <param name="eventItem">The domain event to remove.</param>
    /// <remarks>
    /// If the specified event is not in the list, this method will have no effect.
    /// </remarks>
    public virtual void RemoveDomainEvent(DomainEvent eventItem)
    {
        _domainEvents.Remove(eventItem);
    }

    /// <summary>
    /// Clears all domain events from the current entity's list.
    /// </summary>
    /// <remarks>
    /// Use this method with caution as it will remove all pending events 
    /// that might not have been dispatched yet.
    /// </remarks>
    public virtual void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    /// <summary>
    /// Determines whether two entities are not equal.
    /// </summary>
    /// <param name="left">The first entity to compare.</param>
    /// <param name="right">The second entity to compare.</param>
    /// <returns>true if the entities are not equal; otherwise, false.</returns>
    public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Determines whether two entities are equal.
    /// </summary>
    /// <param name="left">The first entity to compare.</param>
    /// <param name="right">The second entity to compare.</param>
    /// <returns>true if the entities are equal; otherwise, false.</returns>
    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
    {
        return left?.Equals(right) ?? right is null;
    }

    /// <summary>
    /// Determines whether the current entity is equal to another entity.
    /// </summary>
    /// <param name="obj">The object to compare with the current entity.</param>
    /// <returns>true if the current entity is equal to the other entity; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TId> entityItem)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (GetType() != obj.GetType())
            return false;

        if (entityItem.IsNotPersisted() || IsNotPersisted())
            return false;

        return Id.Equals(entityItem.Id);
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current entity.</returns>
    public override int GetHashCode()
    {
        // If the entity is not persisted, just use the base implementation.
        if (IsNotPersisted())
            // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
            return base.GetHashCode();

        // Use cached hash code if available.
        // ReSharper disable once NonReadonlyMemberInGetHashCode
        if (_requestedHashCode != int.MinValue)
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return _requestedHashCode;

        // Compute and cache the hash code using HashCode struct.
        // ReSharper disable once NonReadonlyMemberInGetHashCode
        _requestedHashCode = HashCode.Combine(Id);

        // ReSharper disable once NonReadonlyMemberInGetHashCode
        return _requestedHashCode;
    }

    /// <summary>
    /// Determines whether the entity is not persisted yet by comparing its identifier to the
    /// default value of its type.
    /// </summary>
    /// <returns>
    /// true if the entity's identifier matches the default value of its type, indicating it is not
    /// persisted; otherwise, false.
    /// </returns>
    public bool IsNotPersisted()
    {
        return Id.Equals(default(TId));
    }
}