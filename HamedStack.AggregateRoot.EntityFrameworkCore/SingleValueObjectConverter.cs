using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HamedStack.TheAggregateRoot.EntityFrameworkCore;

public class SingleValueObjectConverter<TSingleValueObject, TValue> : ValueConverter<TSingleValueObject, TValue>
    where TSingleValueObject : SingleValueObject<TValue>, new()
{
    public SingleValueObjectConverter() : base(
        vo => vo.Value,
        value => new TSingleValueObject { Value = value })
    {
    }
}