using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.Policy
{
    public interface ILimitPolicy
    {
        IRuleCollection<Limit> InvariantRules();
    }
}
