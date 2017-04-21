using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.Invariants.Interfaces;

namespace Shuttle.Abacus.Invariants
{
    public class DecimalTableRules : IDecimalTableRules
    {
        public IRuleCollection<object> DecimalTableNameRules()
        {
            return Rule.With().Required().MaximumLength(160).Create();
        }

        public IRuleCollection<object> RowArgumentRules()
        {
            return Rule.With().Required().Create();
        }
    }
}
