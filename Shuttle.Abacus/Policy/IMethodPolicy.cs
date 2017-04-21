using Abacus.Domain;
using Abacus.Validation;

namespace Abacus.Policy
{
    public interface IMethodPolicy
    {
        IRuleCollection<Method> InvariantRules();
    }
}
