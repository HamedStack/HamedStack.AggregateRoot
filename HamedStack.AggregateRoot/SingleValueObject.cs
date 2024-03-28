namespace HamedStack.TheAggregateRoot;

[Serializable]
public class SingleValueObject<T> : ValueObject
{
    protected SingleValueObject()
    {

    }

    public static implicit operator T(SingleValueObject<T> valueObject)
    {
        return valueObject.Value;
    }

    public static implicit operator SingleValueObject<T>(T value)
    {
        return new SingleValueObject<T>() { Value = value };
    }

    public T Value { get; set; } = default!;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}