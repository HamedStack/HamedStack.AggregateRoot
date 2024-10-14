namespace HamedStack.TheAggregateRoot.Abstractions;

/// <summary>
/// Defines a contract for entities that support soft deletion.
/// Soft deletion allows records to be marked as deleted without 
/// actually removing them from the database, enabling recovery and auditing.
/// </summary>
public interface ISoftDelete
{
    /// <summary>
    /// Gets or sets the identifier of the user who deleted the entity.
    /// This property can be null if the entity has not been deleted.
    /// </summary>
    string? DeletedBy { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was marked as deleted.
    /// This property is set when the entity is deleted and provides a timestamp
    /// for auditing purposes.
    /// </summary>
    DateTimeOffset DeletedOn { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the entity is marked as deleted.
    /// This property is typically used to filter out deleted entities from queries.
    /// </summary>
    bool IsDeleted { get; set; }
}