using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.Policy
{
    public interface ISystemUserPolicy
    {
        IRuleCollection<SystemUser> InvariantRules();
    }
}
