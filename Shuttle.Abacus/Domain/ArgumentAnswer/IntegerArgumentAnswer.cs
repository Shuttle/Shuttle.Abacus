using System;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class IntegerArgumentAnswer : ArgumentAnswer
    {
        private readonly int value;

        public IntegerArgumentAnswer(string argumentName, string text) : base(argumentName)
        {
            AnswerString = text;

            value = int.Parse(text);

            Answer = value;
        }

        public IntegerArgumentAnswer(string argumentName, int value) : base(argumentName)
        {
            this.value = value;

            Answer = value;
            AnswerString = Convert.ToString(value);
        }

        public override string AnswerType
        {
            get { return "Integer"; }
        }

        public override int CompareTo(ArgumentAnswer other)
        {
            Guard.AgainstNull(other, "other");

            int otherValue;

            if (!int.TryParse(other.AnswerString, out otherValue))
            {
                throw new InvalidCastException(string.Format(Resources.IncompatibleCalculationValues, GetType().Name,
                                                             other.GetType().Name));
            }

            return value.CompareTo(otherValue);
        }

        public override string DisplayString()
        {
            return value.ToString("#,##0");
        }
    }
}