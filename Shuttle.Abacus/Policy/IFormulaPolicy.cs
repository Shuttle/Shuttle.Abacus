using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Invariants.Core;

namespace Shuttle.Abacus.Policy
{
    public interface IFormulaPolicy
    {
        IRuleCollection<Formula> InvariantRules();
    }
}
