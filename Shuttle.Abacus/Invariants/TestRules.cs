using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.Invariants.Interfaces;

namespace Shuttle.Abacus.Invariants
{
    public class TestRules : ITestRules
    {
        public IRuleCollection<object> NameRules()
        {
            return Rule.With().Required().MaximumLength(250).Create();
        }

        public IRuleCollection<object> FormulaNameRules()
        {
            return Rule.With().Required().MaximumLength(120).Create();
        }
    }
}
