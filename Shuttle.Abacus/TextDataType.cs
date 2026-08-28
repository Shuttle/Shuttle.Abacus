using Shuttle.Contract;

namespace Shuttle.Abacus;

public class TextDataType : DataType
{
    public TextDataType(string text)
    {
        ValueString = text;
        Value = text;
    }

    public override string Name => "Text";

    public override int CompareTo(DataType? other)
    {
        Guard.AgainstNull(other);

        return string.Compare(ValueString, other.ValueString, StringComparison.Ordinal);
    }

    public override string Text()
    {
        return ValueString;
    }
}
