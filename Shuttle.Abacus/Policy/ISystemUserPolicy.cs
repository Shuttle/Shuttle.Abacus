using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Invariants.Core;

namespace Shuttle.Abacus.Policy
{
    public interface ISystemUserPolicy
    {
        IRuleCollection<SystemUser> InvariantRules();
    }
}
