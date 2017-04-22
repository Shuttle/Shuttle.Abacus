using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.Invariants.Core
{
    public interface IRuleResult
    {
        bool OK { get; }
        ResultMessage RootMessage { get; }
        IResult ToResult();
    }
}
