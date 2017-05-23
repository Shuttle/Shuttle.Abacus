using System;
using Shuttle.Abacus.Localisation;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class DateTimeValueType : ValueType
    {
        private readonly DateTime value;

        public DateTimeValueType(string argumentName, string text) : base(argumentName)
        {
            ValueString = text;

            value = DateTime.Parse(text);

            Value = value;
        }

        public override string AnswerType => "DateTime";

        public override int CompareTo(ValueType other)
        {
            Guard.AgainstNull(other, "other");

            DateTime otherValue;

            if (!DateTime.TryParse(other.ValueString, out otherValue))
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