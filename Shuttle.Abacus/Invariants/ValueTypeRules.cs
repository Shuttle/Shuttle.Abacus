using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.Invariants.Interfaces;

namespace Shuttle.Abacus.Invariants
{
    public class ValueTypeRules : IValueTypeRules
    {
        public IRuleCollection<object> DecimalRules()
        {
            return Rule.With().Required().MaximumLength(120).Decimal().Create();
        }

        public IRuleCollection<object> BooleanRules()
        {
            return Rule.With().Required().MaximumLength(120).Boolean().Create();
        }

        public IRuleCollection<object> DateTimeRules()
        {
            return Rule.With().Required().MaximumLength(120).DateTime().Create();
        }

        public IRuleCollection<object> IntegerRules()
        {
            return Rule.With().Required().MaximumLength(120).Integer().Create();
        }

        public IRuleCollection<object> TextRules()
        {
            return Rule.With().Required().MaximumLength(120).Create();
        }

        public IRuleCollection<object> For(string type)
        {
            switch ((type ?? string.Empty).ToLowerInvariant())
            {
                case "boolean":
                    {
                        return BooleanRules();
                    }
                case "decimal":
                    {
                        return DecimalRules();
                    }
                case "date":
                case "datetime":
                    {
                        return DateTimeRules();
                    }
                case "integer":
                    {
                        return IntegerRules();
                    }
                default:
                    {
                        return TextRules();
                    }
            }
        }
    }
}
