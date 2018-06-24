using Shuttle.Core.Contract;

namespace Shuttle.Abacus
{
    public class TextDataType : DataType
    {
        public TextDataType(string text)
        {
            ValueString = text;
            Value = text;
        }

        public override string Name => "Text";

        public override int CompareTo(DataType other)
        {
            Guard.AgainstNull(other, nameof(other));

            return ValueString.CompareTo(other.ValueString);
        }

        public override string Text()
        {
            return ValueString;
        }
    }
}