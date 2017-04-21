namespace Shuttle.Abacus.Infrastructure
{
    public interface IValueReturningVisitor<TValueToReturn, T> : IVisitor<T>
    {
        TValueToReturn GetResult();
    }
}
