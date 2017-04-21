using Abacus.Domain;
using Abacus.Validation;

namespace Abacus.Policy
{
    public interface ICalculationPolicy
    {
        IRuleCollection<Calculation> InvariantRules();
    }
}
