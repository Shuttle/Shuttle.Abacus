using System.Globalization;
using Shuttle.Contract;

namespace Shuttle.Abacus;

public class DateTimeDataType : DataType
{
    private readonly DateTime _value;

    public DateTimeDataType(string text)
    {
        ValueString = text;
        _value = DateTime.Parse(text, CultureInfo.InvariantCulture);
        Value = _value;
    }

    public override string Name => "DateTime";

    public override int CompareTo(DataType? other)
    {
        Guard.AgainstNull(other);

        if (!DateTime.TryParse(other.ValueString, CultureInfo.InvariantCulture, DateTimeStyles.None, out var otherValue))
        {
            throw new InvalidCastException(string.Format(Resources.IncompatibleDataTypes, GetType().Name, other.GetType().Name));
        }

        return _value.CompareTo(otherValue);
    }

    public override string Text()
    {
        return _value.ToString("dd MMM yyyy HH:mm:ss", CultureInfo.InvariantCulture);
    }
}
