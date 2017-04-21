namespace Shuttle.Abacus.Infrastructure
{
    public interface IPipe<T>
    {
        void Handle(T item);
    }
}
