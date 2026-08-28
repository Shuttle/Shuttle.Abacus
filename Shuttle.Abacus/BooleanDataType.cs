using Shuttle.Contract;

namespace Shuttle.Abacus;

public class BooleanDataType : DataType
{
    private readonly bool _value;

    public BooleanDataType(bool value)
    {
        _value = value;
        Value = value;
        ValueString = Convert.ToString(value);
    }

    public BooleanDataType(string text)
    {
        ValueString = text;

        _value = text.ToLowerInvariant() switch
        {
            "1" or "yes" or "true" or "y" or "t" => true,
            _ => false
        };

        Value = _value;
    }

    public override string Name => "Boolean";

    public override int CompareTo(DataType? other)
    {
        Guard.AgainstNull(other);

        if (!bool.TryParse(other.ValueString, out var otherValue))
        {
            throw new InvalidCastException(string.Format(Resources.IncompatibleDataTypes, GetType().Name, other.GetType().Name));
        }

        return _value.CompareTo(otherValue);
    }

    public override string Text()
    {
        return _value ? "Yes" : "No";
    }
}
