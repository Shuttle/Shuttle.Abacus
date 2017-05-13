using System;

namespace Shuttle.Abacus.Infrastructure
{
    public sealed class Enumeration
    {
        public enum CalculationOwnerType
        {
            Method = 0,
            Calculation = 1,
            Limit = 2
        }

        public enum CalculationType
        {
            Formula = 0,
            Collection = 1
        }

        public enum InputType
        {
            Boolean = 0,
            Date = 1,
            Decimal = 2,
            Integer = 3,
            List = 4,
            Money = 5,
            Text = 6
        }

        public enum ValueSourceType
        {
            ArgumentAnswer = 0,
            Decimal = 1,
            RunningTotal = 2,
            FormulaTotal = 5,
            Matrix = 6,
            FormulaResult = 7
        }

        public static TEnumeration Cast<TEnumeration>(int? value)
        {
            return value.HasValue
                      ? (TEnumeration)Enum.Parse(typeof(TEnumeration), value.Value.ToString())
                      : default(TEnumeration);
        }

        public static TEnumeration Cast<TEnumeration>(int value)
        {
            return (TEnumeration)Enum.Parse(typeof(TEnumeration), value.ToString());
        }

        public static TEnumeration Cast<TEnumeration>(string name)
        {
            return (TEnumeration)Enum.Parse(typeof(TEnumeration), name);
        }
    }
}
