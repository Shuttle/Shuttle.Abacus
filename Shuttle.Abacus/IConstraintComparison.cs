namespace Shuttle.Abacus
{
    public interface IConstraintComparison
    {
        bool IsSatisfiedBy(string type, string argumentValue, string comparison, string constraintValue);
    }
}