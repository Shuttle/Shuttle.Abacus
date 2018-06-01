using System;
using Shuttle.Core.Contract;

namespace Shuttle.Abacus
{
    public class IntegerValueType : ValueType
    {
        private readonly int _value;

        public IntegerValueType(string text)
        {
            ValueString = text;

            _value = int.Parse(text);

            Value = _value;
        }

        public IntegerValueType(int value)
        {
            _value = value;

            Value = value;
            ValueString = Convert.ToString(value);
        }

        public override string AnswerType => "Integer";

        public override int CompareTo(ValueType other)
        {
            Guard.AgainstNull(other, "other");

            int otherValue;

            if (!int.TryParse(other.ValueString, out otherValue))
            {
                throw new InvalidCastException(string.Format(Resources.IncompatibleCalculationValues, GetType().Name,
                    other.GetType().Name));
            }

            return _value.CompareTo(otherValue);
        }

        public override string DisplayString()
        {
            return _value.ToString("#,##0");
        }
    }
}