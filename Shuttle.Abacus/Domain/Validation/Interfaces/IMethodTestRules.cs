namespace Shuttle.Abacus.Domain
{
    public interface IMethodTestRules
    {
        IRuleCollection<object> DescriptionRules();
        IRuleCollection<object> ExpectedResultRules();
    }
}
