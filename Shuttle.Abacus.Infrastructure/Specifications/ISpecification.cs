namespace Shuttle.Abacus.Infrastructure
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T item);
    }
}
