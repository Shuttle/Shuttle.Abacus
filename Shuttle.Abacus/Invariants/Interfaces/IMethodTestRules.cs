namespace Abacus.Validation
{
    public interface IMethodTestRules
    {
        IRuleCollection<object> DescriptionRules();
        IRuleCollection<object> ExpectedResultRules();
    }
}
