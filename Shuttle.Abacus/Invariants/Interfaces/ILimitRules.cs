using Shuttle.Abacus.Invariants.Core;

namespace Shuttle.Abacus.Invariants.Interfaces
{
    public interface ILimitRules
    {
        IRuleCollection<object> LimitNameRules();
        IRuleCollection<object> TypeRules();
    }
}
