namespace Shuttle.Abacus.Domain
{
    public interface IConstraintComparison
    {
        bool IsSatisfiedBy(string type, string argumentValue, string comparison, string constraintValue);
    }
}