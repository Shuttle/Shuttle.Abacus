namespace Shuttle.Abacus
{
    public interface IConstraintComparison
    {
        bool IsSatisfiedBy(string dataTypeName, string argumentValue, string comparison, string constraintValue);
    }
}