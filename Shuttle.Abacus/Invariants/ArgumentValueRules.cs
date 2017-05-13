using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.Invariants.Interfaces;

namespace Shuttle.Abacus.Invariants
{
    public class ArgumentValueRules : IArgumentValueRules
    {
        public IRuleCollection<object> ValueRules()
        {
            return Rule.With().Required().Create();
        }
    }
}
