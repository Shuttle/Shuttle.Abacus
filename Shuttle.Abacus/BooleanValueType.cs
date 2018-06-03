using System;
using Shuttle.Core.Contract;

namespace Shuttle.Abacus
{
    public class BooleanValueType : ValueType
    {
        private readonly bool value;

        public BooleanValueType(bool value)
        {
            Value = value;
            ValueString = Convert.ToString(value);
        }

        public BooleanValueType(string text)
        {
            ValueString = text;

            switch (text.ToLower())
            {
                case "1":
                case "yes":
                case "true":
                case "y":
                case "t":
                {
                    value = true;
                    break;
                }
                default:
                {
                    value = false;
                    break;
                }
            }

            Value = value;
        }

        public override string AnswerType => "Boolean";

        public override int CompareTo(ValueType other)
        {
            Guard.AgainstNull(other, nameof(other));

            bool otherValue;

            if (!bool.TryParse(other.ValueString, out otherValue))
            {
                throw new InvalidCastException(string.Format(Resources.IncompatibleCalculationValues, GetType().Name,
                    other.GetType().Name));
            }

            return value.CompareTo(otherValue);
        }

        public override string DisplayString()
        {
            return value ? "Yes" : "No";
        }
    }
}