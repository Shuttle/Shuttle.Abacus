using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.Invariants.Core
{
    public interface IRuleResult
    {
        bool OK { get; }
        ResultMessage RootMessage { get; }
        IResult ToResult();
    }
}
