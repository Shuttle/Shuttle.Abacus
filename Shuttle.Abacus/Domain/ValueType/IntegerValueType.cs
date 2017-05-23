using System;
using Shuttle.Abacus.Localisation;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class IntegerValueType : ValueType
    {
        private readonly int value;

        public IntegerValueType(string argumentName, string text) : base(argumentName)
        {
            ValueString = text;

            value = int.Parse(text);

            Value = value;
        }

        public IntegerValueType(string argumentName, int value) : base(argumentName)
        {
            this.value = value;

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

            return value.CompareTo(otherValue);
        }

        public override string DisplayString()
        {
            return value.ToString("#,##0");
        }
    }
}