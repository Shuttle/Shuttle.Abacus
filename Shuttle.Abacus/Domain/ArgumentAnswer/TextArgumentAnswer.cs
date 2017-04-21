using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class TextArgumentAnswer : ArgumentAnswer
    {
        public TextArgumentAnswer(string argumentName, string text) : base(argumentName)
        {
            AnswerString = text;
            Answer = text;
        }

        public override string AnswerType
        {
            get { return "Text"; }
        }

        public override int CompareTo(ArgumentAnswer other)
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