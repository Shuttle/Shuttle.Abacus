using System;
using Shuttle.Abacus.Localisation;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class DecimalArgumentAnswer : ArgumentAnswer
    {
        private readonly decimal value;

        public DecimalArgumentAnswer(string argumentName, decimal value) : base(argumentName)
        {
            this.value = value;

            AnswerString = Convert.ToString(value);
            Answer = value;
        }

        public DecimalArgumentAnswer(string argumentName, string text) : base(argumentName)
        {
            AnswerString = text;

            value = decimal.Parse(text);

            Answer = value;
        }

        public override string AnswerType
        {
            get { return "Decimal"; }
        }

        public override int CompareTo(ArgumentAnswer other)
        {
            Guard.AgainstNull(other, "other");

            decimal otherValue;

            if (!decimal.TryParse(other.AnswerString, out otherValue))
            {
                throw new InvalidCastException(string.Format(Resources.IncompatibleCalculationValues, GetType().Name,
                                                             other.GetType().Name));
            }

            return value.CompareTo(otherValue);
        }

        public override string DisplayString()
        {
            return value.ToString("N");
        }
    }
}