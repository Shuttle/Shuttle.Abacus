using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.Invariants.Interfaces;

namespace Shuttle.Abacus.Invariants
{
    public class SystemUserRules : ISystemUserRules
    {
        public IRuleCollection<object> LoginNameRules()
        {
            return Rule.With().Required().MaximumLength(100).Create();
        }
    }
}
