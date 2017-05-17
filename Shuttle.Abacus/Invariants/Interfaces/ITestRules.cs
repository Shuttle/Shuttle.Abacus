using Shuttle.Abacus.Invariants.Core;

namespace Shuttle.Abacus.Invariants.Interfaces
{
    public interface ITestRules
    {
        IRuleCollection<object> NameRules();
        IRuleCollection<object> FormulaNameRules();
    }
}
