using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class TextValueType : ValueType
    {
        public TextValueType(string argumentName, string text) : base(argumentName)
        {
            AnswerString = text;
            Answer = text;
        }

        public override string AnswerType => "Text";

        public override int CompareTo(ValueType other)
        {
            Guard.AgainstNull(other, "other");

            return AnswerString.CompareTo(other.AnswerString);
        }

        public override string DisplayString()
        {
            return AnswerString;
        }
    }
}