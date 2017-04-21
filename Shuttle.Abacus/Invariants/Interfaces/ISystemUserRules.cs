using Shuttle.Abacus.Invariants.Core;

namespace Shuttle.Abacus.Invariants.Interfaces
{
    public interface ISystemUserRules
    {
        IRuleCollection<object> LoginNameRules();
    }
}
