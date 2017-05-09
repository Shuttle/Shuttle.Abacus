using Shuttle.Abacus.Invariants.Core;

namespace Shuttle.Abacus.Invariants.Interfaces
{
    public interface IMatrixRules
    {
        IRuleCollection<object> DecimalTableNameRules();
        IRuleCollection<object> RowArgumentRules();
    }
}
