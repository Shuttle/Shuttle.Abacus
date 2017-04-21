using Abacus.Domain;
using Abacus.Validation;

namespace Abacus.Policy
{
    public interface ILimitPolicy
    {
        IRuleCollection<Limit> InvariantRules();
    }
}
