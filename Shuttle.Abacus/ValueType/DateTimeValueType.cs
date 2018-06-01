using System;
using Shuttle.Core.Contract;

namespace Shuttle.Abacus
{
    public class DateTimeValueType : ValueType
    {
        private readonly DateTime _value;

        public DateTimeValueType(string text)
        {
            ValueString = text;

            _value = DateTime.Parse(text);

            Value = _value;
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

            return _value.CompareTo(otherValue);
        }

        public override string DisplayString()
        {
            return _value.ToString("dd MMM yyyy HH:mm:ss");
        }
    }
}