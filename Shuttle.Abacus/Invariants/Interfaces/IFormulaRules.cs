using Shuttle.Abacus.Invariants.Core;

namespace Shuttle.Abacus.Invariants
{
    public interface IFormulaRules
    {
        IRuleCollection<object> FormulaNameRules();
        IRuleCollection<object> ExecutionTypeRules();
    }
}