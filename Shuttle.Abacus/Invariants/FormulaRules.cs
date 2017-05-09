using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.Invariants.Interfaces;

namespace Shuttle.Abacus.Invariants
{
    public class FormulaRules : IFormulaRules
    {
        public IRuleCollection<object> FormulaNameRules()
        {
            return Rule.With().Required().MaximumLength(120).Create();
        }

        public IRuleCollection<object> ExecutionTypeRules()
        {
            return Rule.With().Required().Create();
        }
    }
}
