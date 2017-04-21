using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.Invariants.Interfaces;

namespace Shuttle.Abacus.Invariants
{
    public class LimitRules : ILimitRules
    {
        public IRuleCollection<object> LimitNameRules()
        {
            return Rule.With().Required().MaximumLength(100).Create();
        }

        public IRuleCollection<object> TypeRules()
        {
            return Rule.With().Required().Create();
        }
    }
}
