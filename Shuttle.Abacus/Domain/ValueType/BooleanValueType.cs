using System;
using Shuttle.Abacus.Localisation;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class BooleanValueType : ValueType
    {
        private readonly bool value;

        public BooleanValueType(string argumentName, bool value)
            : base(argumentName)
        {
            Answer = value;
            AnswerString = Convert.ToString(value);
        }

        public BooleanValueType(string argumentName, string text)
            : base(argumentName)
        {
            AnswerString = text;

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

            Answer = value;
        }

        public override string AnswerType => "Boolean";

        public override int CompareTo(ValueType other)
        {
            Guard.AgainstNull(other, "other");

            bool otherValue;

            if (!bool.TryParse(other.AnswerString, out otherValue))
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