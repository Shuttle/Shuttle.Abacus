namespace Shuttle.Abacus.Domain
{
    public interface IRuleResult
    {
        bool OK { get; }
        ResultMessage RootMessage { get; }
        IResult ToResult();
    }
}
