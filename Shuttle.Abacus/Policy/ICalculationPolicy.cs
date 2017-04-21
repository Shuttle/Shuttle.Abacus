using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.Policy
{
    public interface ICalculationPolicy
    {
        IRuleCollection<Calculation> InvariantRules();
    }
}
