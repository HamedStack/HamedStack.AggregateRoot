namespace HamedStack.TheAggregateRoot;

[Serializable]
public abstract class ValueObject
{
    protected ValueObject(){}

    /// <summary>
    /// Gets the components that should be used for equality checks.
    /// </summary>
    /// <returns>An enumerable of the components to be checked for equality.</returns>
    protected abstract IEnumerable<object?> GetEqualityComponents();

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        switch (obj)
        {
            case null:
                return false;
        }

        if (GetType() != obj.GetType())
            return false;

        var valueObject = (ValueObject)obj;

        return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Aggregate(1, (current, obj) =>
            {
                unchecked
                {
                    return current * 23 + (obj?.GetHashCode() ?? 0);
                }
            });
    }

    /// <summary>
    /// Determines whether two specified value objects have the same value.
    /// </summary>
    /// <param name="a">The first value object to compare, or null.</param>
    /// <param name="b">The second value object to compare, or null.</param>
    /// <returns>true if the value of a is the same as the value of b; otherwise, false.</returns>
    public static bool operator ==(ValueObject? a, ValueObject? b)
    {
        return a switch
        {
            null when b is null => true,
            _ => a is not null && b is not null && a.Equals(b)
        };
    }

    /// <summary>
    /// Determines whether two specified value objects have different values.
    /// </summary>
    /// <param name="a">The first value object to compare, or null.</param>
    /// <param name="b">The second value object to compare, or null.</param>
    /// <returns>true if the value of a is different from the value of b; otherwise, false.</returns>
    public static bool operator !=(ValueObject? a, ValueObject? b)
    {
        return !(a == b);
    }

    /// <summary>
    /// Creates a shallow copy of the current <see cref="ValueObject"/>.
    /// </summary>
    /// <returns>
    /// A new <see cref="ValueObject"/> that is a shallow copy of this instance.
    /// </returns>
    /// <remarks>
    /// This method relies on the <see cref="object.MemberwiseClone"/> method 
    /// to produce a shallow copy of the current object. Derived properties 
    /// and fields will be referenced rather than duplicated. 
    /// If deep cloning is desired, a different implementation would be necessary.
    /// </remarks>
    public ValueObject? GetCopy()
    {
        return MemberwiseClone() as ValueObject;
    }

    /// <summary>
    /// Compares the current instance with another object of the same type 
    /// and returns an integer that indicates whether the current instance 
    /// precedes, follows, or occurs in the same position in the sort order 
    /// as the other object.
    /// </summary>
    /// <param name="obj">An object to compare with this instance.</param>
    /// <returns>
    /// A value that indicates the relative order of the objects being compared. 
    /// The return value has these meanings: 
    /// Less than zero: This instance precedes `obj` in the sort order. 
    /// Zero: This instance occurs in the same position in the sort order as `obj`.
    /// Greater than zero: This instance follows `obj` in the sort order.
    /// </returns>
    public int CompareTo(object? obj)
    {
        switch (obj)
        {
            case null:
                return 1;
        }

        var thisType = GetUnproxiedType(this);
        var otherType = GetUnproxiedType(obj);

        if (thisType != otherType)
            return string.Compare(thisType?.ToString(), otherType?.ToString(), StringComparison.Ordinal);

        var other = (ValueObject)obj;

        var components = GetEqualityComponents().ToArray();
        var otherComponents = other.GetEqualityComponents().ToArray();

        for (var i = 0; i < components.Length; i++)
        {
            var comparison = CompareComponents(components[i], otherComponents[i]);
            if (comparison != 0)
                return comparison;
        }

        return 0;
    }

    /// <summary>
    /// Compares two objects and returns an integer that indicates their 
    /// relative position in the sort order.
    /// </summary>
    /// <param name="object1">The first object to compare.</param>
    /// <param name="object2">The second object to compare.</param>
    /// <returns>
    /// A value that indicates the relative order of the objects being compared.
    /// </returns>
    private static int CompareComponents(object? object1, object? object2)
    {
        return object1 switch
        {
            null when object2 is null => 0,
            null => -1,
            _ => object2 switch
            {
                null => 1,
                _ => object1 is IComparable comparable1 && object2 is IComparable comparable2
                    ? comparable1.CompareTo(comparable2)
                    : object1.Equals(object2)
                        ? 0
                        : -1
            }
        };
    }

    /// <summary>
    /// Compares the current instance with another instance of the same type 
    /// and returns an integer that indicates whether the current instance 
    /// precedes, follows, or occurs in the same position in the sort order 
    /// as the other instance.
    /// </summary>
    /// <param name="other">An instance of <see cref="ValueObject"/> to compare with this instance.</param>
    /// <returns>
    /// A value that indicates the relative order of the instances being compared. 
    /// The return value has these meanings: 
    /// Less than zero: This instance precedes `other` in the sort order. 
    /// Zero: This instance occurs in the same position in the sort order as `other`.
    /// Greater than zero: This instance follows `other` in the sort order.
    /// </returns>
    public int CompareTo(ValueObject? other)
    {
        return CompareTo(other as object);
    }

    /// <summary>
    /// Retrieves the unproxied type of an object, especially useful when dealing with ORMs like Entity Framework Core or NHibernate that 
    /// use proxies for entities.
    /// </summary>
    /// <param name="obj">The object whose type needs to be determined.</param>
    /// <returns>The actual, unproxied type of the given object.</returns>
    /// <remarks>
    /// This method checks for typical proxy naming conventions used by Entity Framework Core and NHibernate. 
    /// For Entity Framework Core, it checks if the type name starts with "Castle.Proxies.".
    /// For NHibernate, it checks if the type name ends with "Proxy".
    /// If the object type matches either of these patterns, the method returns the base type, which should be the actual entity type.
    /// </remarks>
    /// <exception cref="ArgumentNullException">Thrown when the input object is null.</exception>
    private static Type? GetUnproxiedType(object obj)
    {
        ArgumentNullException.ThrowIfNull(obj);

        const string EFCoreProxyPrefix = "Castle.Proxies.";
        const string NHibernateProxyPostfix = "Proxy";

        var type = obj.GetType();
        var typeString = type.ToString();

        return typeString.Contains(EFCoreProxyPrefix) || typeString.EndsWith(NHibernateProxyPostfix)
            ? type.BaseType
            : type;
    }
}