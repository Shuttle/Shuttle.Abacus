using System.Globalization;
using Shuttle.Contract;

namespace Shuttle.Abacus;

public class IntegerDataType : DataType
{
    private readonly int _value;

    public IntegerDataType(string text)
    {
        ValueString = text;
        _value = int.Parse(text, CultureInfo.InvariantCulture);
        Value = _value;
    }

    public IntegerDataType(int value)
    {
        _value = value;
        Value = value;
        ValueString = Convert.ToString(value, CultureInfo.InvariantCulture);
    }

    public override string Name => "Integer";

    public override int CompareTo(DataType? other)
    {
        Guard.AgainstNull(other);

        if (!int.TryParse(other.ValueString, NumberStyles.Integer, CultureInfo.InvariantCulture, out var otherValue))
        {
            throw new InvalidCastException(string.Format(Resources.IncompatibleDataTypes, GetType().Name, other.GetType().Name));
        }

        return _value.CompareTo(otherValue);
    }

    public override string Text()
    {
        return _value.ToString("#,##0", CultureInfo.InvariantCulture);
    }
}
