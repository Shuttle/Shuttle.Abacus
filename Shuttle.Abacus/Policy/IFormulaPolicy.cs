using Abacus.Domain;
using Abacus.Validation;

namespace Abacus.Policy
{
    public interface IFormulaPolicy
    {
        IRuleCollection<Formula> InvariantRules();
    }
}
