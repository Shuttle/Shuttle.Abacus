using Shuttle.Abacus.Invariants.Core;

namespace Shuttle.Abacus.Invariants.Interfaces
{
    public interface IDecimalTableRules
    {
        IRuleCollection<object> DecimalTableNameRules();
        IRuleCollection<object> RowArgumentRules();
    }
}
