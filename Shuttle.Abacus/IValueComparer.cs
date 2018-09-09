namespace Shuttle.Abacus
{
    public interface IValueComparer
    {
        bool IsSatisfiedBy(string dataTypeName, string value, string comparison, string comparisonValue);
    }
}