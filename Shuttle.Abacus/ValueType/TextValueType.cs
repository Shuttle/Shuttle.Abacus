using Shuttle.Core.Contract;

namespace Shuttle.Abacus
{
    public class TextValueType : ValueType
    {
        public TextValueType(string text)
        {
            ValueString = text;
            Value = text;
        }

        public override string AnswerType => "Text";

        public override int CompareTo(ValueType other)
        {
            Guard.AgainstNull(other, "other");

            return ValueString.CompareTo(other.ValueString);
        }

        public override string DisplayString()
        {
            return ValueString;
        }
    }
}