namespace Shuttle.Abacus
{
    public interface IMethodTestRules
    {
        IRuleCollection<object> DescriptionRules();
        IRuleCollection<object> ExpectedResultRules();
    }
}
