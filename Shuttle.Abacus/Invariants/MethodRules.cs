using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.Invariants.Interfaces;

namespace Shuttle.Abacus.Invariants
{
    public class MethodRules : IMethodRules
    {
        public IRuleCollection<object> MethodNameRules()
        {
            return Rule.With().Required().MaximumLength(50).Create();
        }
    }
}
