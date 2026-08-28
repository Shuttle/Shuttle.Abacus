using System.Globalization;
using Shuttle.Contract;

namespace Shuttle.Abacus;

public class DecimalDataType : DataType
{
    private readonly decimal _value;

    public DecimalDataType(decimal value)
    {
        _value = value;
        ValueString = Convert.ToString(value, CultureInfo.InvariantCulture);
        Value = value;
    }

    public DecimalDataType(string text)
    {
        ValueString = text;
        _value = decimal.Parse(text, CultureInfo.InvariantCulture);
        Value = _value;
    }

    public override string Name => "Decimal";

    public override int CompareTo(DataType? other)
    {
        Guard.AgainstNull(other);

        if (!decimal.TryParse(other.ValueString, NumberStyles.Number, CultureInfo.InvariantCulture, out var otherValue))
        {
            throw new InvalidCastException(string.Format(Resources.IncompatibleDataTypes, GetType().Name, other.GetType().Name));
        }

        return _value.CompareTo(otherValue);
    }

    public override string Text()
    {
        return _value.ToString("N", CultureInfo.InvariantCulture);
    }
}
