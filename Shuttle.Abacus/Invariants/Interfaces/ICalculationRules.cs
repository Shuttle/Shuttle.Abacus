using Shuttle.Abacus.Invariants.Core;

namespace Shuttle.Abacus.Invariants.Interfaces
{
    public interface ICalculationRules
    {
        IRuleCollection<object> CalculationNameRules();
        IRuleCollection<object> TypeRules();
    }
}
