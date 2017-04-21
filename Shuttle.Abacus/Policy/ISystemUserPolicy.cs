using Abacus.Domain;
using Abacus.Validation;

namespace Abacus.Policy
{
    public interface ISystemUserPolicy
    {
        IRuleCollection<SystemUser> InvariantRules();
    }
}
