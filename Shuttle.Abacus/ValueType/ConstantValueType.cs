using System;
using Shuttle.Core.Contract;

namespace Shuttle.Abacus
{
    public class ConstantValueType : ValueType
    {
        private readonly decimal _value;

        public ConstantValueType(decimal value)
        {
            _value = value;

            ValueString = Convert.ToString(value);
            Value = value;
        }

        public ConstantValueType(string text)
        {
            ValueString = text;

            _value = decimal.Parse(text);

            Value = _value;
        }

        public override string AnswerType => "Constant";

        public override int CompareTo(ValueType other)
        {
            Guard.AgainstNull(other, "other");

            decimal otherValue;

            if (!decimal.TryParse(other.ValueString, out otherValue))
            {
                throw new InvalidCastException(string.Format(Resources.IncompatibleCalculationValues, GetType().Name,
                    other.GetType().Name));
            }

            return _value.CompareTo(otherValue);
        }

        public override string DisplayString()
        {
            return _value.ToString("N");
        }
    }
}