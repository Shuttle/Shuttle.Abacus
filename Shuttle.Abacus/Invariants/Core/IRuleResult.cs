using Abacus.Infrastructure;

namespace Abacus.Validation
{
    public interface IRuleResult
    {
        bool OK { get; }
        ResultMessage RootMessage { get; }
        IResult ToResult();
    }
}
