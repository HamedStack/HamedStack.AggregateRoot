namespace HamedStack.TheAggregateRoot;

/// <summary>
/// Represents a single value object, providing a way to treat a simple value as an object with full
/// value semantics, such as equality checks and implicit conversion capabilities. This class is
/// particularly useful in domain-driven design (DDD) to encapsulate single value identities or concepts.
/// </summary>
/// <typeparam name="T">The type of the value this object encapsulates.</typeparam>
[Serializable]
public class SingleValueObject<T> : ValueObject
{
    /// <summary>
    /// Protected constructor used to initialize the SingleValueObject class without setting its value.
    /// It is intended to be used by derived classes.
    /// </summary>
    protected SingleValueObject()
    {
    }

    /// <summary>
    /// Initializes a new instance of the SingleValueObject class with the specified value.
    /// </summary>
    /// <param name="value">The value to encapsulate.</param>
    public SingleValueObject(T value)
    {
        Value = value;
    }

    /// <summary>
    /// Allows implicit conversion from SingleValueObject&lt;T&gt; to T.
    /// </summary>
    /// <param name="valueObject">The SingleValueObject to convert.</param>
    /// <returns>The encapsulated value of type T.</returns>
    public static implicit operator T(SingleValueObject<T> valueObject)
    {
        return valueObject.Value;
    }

    /// <summary>
    /// Allows implicit conversion from T to SingleValueObject&lt;T&gt;.
    /// </summary>
    /// <param name="value">The value to encapsulate.</param>
    /// <returns>A new instance of SingleValueObject encapsulating the specified value.</returns>
    public static implicit operator SingleValueObject<T>(T value)
    {
        return new SingleValueObject<T>(value);
    }

    /// <summary>
    /// Gets or sets the value encapsulated by this object.
    /// </summary>
    public T Value { get; set; } = default!;

    /// <summary>
    /// Provides the components for value-based equality checks implemented by the base ValueObject.
    /// </summary>
    /// <returns>An IEnumerable of objects to use for equality checks.</returns>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
