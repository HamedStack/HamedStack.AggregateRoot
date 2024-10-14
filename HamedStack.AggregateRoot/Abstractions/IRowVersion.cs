// ReSharper disable CommentTypo
namespace HamedStack.TheAggregateRoot.Abstractions;

/// <summary>
/// Defines a contract for entities that support row versioning,
/// typically used for concurrency control in data operations.
/// </summary>
public interface IRowVersion
{
    /// <summary>
    /// Gets or sets the row version for the entity.
    /// This property is typically a byte array representing 
    /// the version of the data in the database to manage concurrency.
    /// </summary>
    byte[] RowVersion { get; set; }
}