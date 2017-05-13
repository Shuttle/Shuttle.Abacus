using Shuttle.Abacus.Invariants.Core;

namespace Shuttle.Abacus.Invariants.Interfaces
{
    public interface IArgumentValueRules
    {
        IRuleCollection<object> ValueRules();
    }
}
