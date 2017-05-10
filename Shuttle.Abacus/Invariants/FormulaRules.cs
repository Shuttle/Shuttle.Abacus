using Shuttle.Abacus.Invariants.Core;

namespace Shuttle.Abacus.Invariants
{
    public class FormulaRules : IFormulaRules
    {
        public IRuleCollection<object> FormulaNameRules()
        {
            return Rule.With().Required().MaximumLength(120).Create();
        }
    }
}