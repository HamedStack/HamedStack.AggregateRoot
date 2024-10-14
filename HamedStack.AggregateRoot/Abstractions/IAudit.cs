namespace HamedStack.TheAggregateRoot.Abstractions;

/// <summary>
/// Defines a contract for entities that support auditing.
/// The <see cref="IAudit"/> interface is used to track the creation and modification 
/// details of an entity, including who created or modified it and when.
/// </summary>
public interface IAudit
{
    /// <summary>
    /// Gets or sets the identifier of the user who created the entity.
    /// This property can be null if the entity was created by an automated process.
    /// </summary>
    string? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// This property provides a timestamp for auditing purposes.
    /// </summary>
    DateTimeOffset CreatedOn { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who last modified the entity.
    /// This property can be null if the entity has never been modified or 
    /// was modified by an automated process.
    /// </summary>
    string? ModifiedBy { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was last modified.
    /// This property is optional and can be null if the entity has never been modified.
    /// </summary>
    DateTimeOffset? ModifiedOn { get; set; }
}