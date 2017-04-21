namespace Shuttle.Abacus.Infrastructure
{
    public interface IVisitor<T>
    {
        void Visit(T item);
    }
}
