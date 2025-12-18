namespace Pertamina.SolutionTemplate.Base.Common;

public abstract class ValueObject
{
    protected static bool EqualOperator(ValueObject? left, ValueObject? right)
    {
        return !(left is null ^ right is null) && (left is null || left.Equals(right));
    }

    protected static bool NotEqualOperator(ValueObject? left, ValueObject? right)
    {
        return !EqualOperator(left, right);
    }

    protected abstract IEnumerable<object> GetEqualityComponents();

#pragma warning disable 8765
    //public override bool Equals(object obj)
    //{
    //    // Ensure the object is not null and is of the same type
    //    if (obj == null)
    //    {
    //        return false;
    //    }

    //    if (obj.GetType() != GetType())
    //    {
    //        return false;
    //    }

    //    // Cast the object to the correct type (ValueObject)
    //    var other = (ValueObject)obj;

    //    // Compare the equality components
    //    return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    //}
#pragma warning restore 8765

    //public override int GetHashCode()
    //{
    //    // Use the XOR operation to combine the hash codes of all equality components
    //    return GetEqualityComponents()
    //        .Select(component => component?.GetHashCode() ?? 0) // Handle nulls safely
    //        .Aggregate(0, (currentHashCode, componentHashCode) => currentHashCode ^ componentHashCode); // XOR all hash codes together
    //}
}
