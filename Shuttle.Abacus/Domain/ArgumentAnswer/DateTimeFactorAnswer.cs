using System;
using Shuttle.Abacus.Localisation;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class DateTimeArgumentAnswer : ArgumentAnswer
    {
        private readonly DateTime value;

        public DateTimeArgumentAnswer(string argumentName, string text) : base(argumentName)
        {
            AnswerString = text;

            value = DateTime.Parse(text);

            Answer = value;
        }

        public override string AnswerType => "DateTime";

        public override int CompareTo(ArgumentAnswer other)
        {
            Guard.AgainstNull(other, "other");

            DateTime otherValue;

            if (!DateTime.TryParse(other.AnswerString, out otherValue))
            {
                throw new InvalidCastException(string.Format(Resources.IncompatibleCalculationValues, GetType().Name,
                                                             other.GetType().Name));
            }

            return value.CompareTo(otherValue);
        }

        public override string DisplayString()
        {
            return value.ToString("dd MMM yyyy HH:mm:ss");
        }
    }
}