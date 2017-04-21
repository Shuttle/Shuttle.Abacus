using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.Policy
{
    public interface IMethodPolicy
    {
        IRuleCollection<Method> InvariantRules();
    }
}
