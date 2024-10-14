using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HamedStack.TheAggregateRoot.EntityFrameworkCore;

/// <summary>
/// A value converter for single value objects, used to convert between 
/// a <typeparamref name="TSingleValueObject"/> and its underlying value of type 
/// <typeparamref name="TValue"/>. This class inherits from <see cref="ValueConverter{TModel, TProvider}"/>.
/// </summary>
/// <typeparam name="TSingleValueObject">The type of the single value object, which must derive from <see cref="SingleValueObject{TValue}"/>.</typeparam>
/// <typeparam name="TValue">The type of the value held by the single value object.</typeparam>
public class SingleValueObjectConverter<TSingleValueObject, TValue> : ValueConverter<TSingleValueObject, TValue>
    where TSingleValueObject : SingleValueObject<TValue>, new()
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SingleValueObjectConverter{TSingleValueObject, TValue}"/> class.
    /// The converter is configured to convert the single value object to its underlying value
    /// and vice versa.
    /// </summary>
    public SingleValueObjectConverter() : base(
        vo => vo.Value,                   // Convert from single value object to its underlying value
        value => new TSingleValueObject { Value = value }) // Convert from underlying value to single value object
    {
    }
}