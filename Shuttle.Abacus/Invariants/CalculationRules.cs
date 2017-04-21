using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.Invariants.Interfaces;

namespace Shuttle.Abacus.Invariants
{
    public class CalculationRules : ICalculationRules
    {
        public IRuleCollection<object> CalculationNameRules()
        {
            return Rule.With().Required().MaximumLength(50).Create();
        }

        public IRuleCollection<object> TypeRules()
        {
            return Rule.With().Required().Create();
        }
    }
}
