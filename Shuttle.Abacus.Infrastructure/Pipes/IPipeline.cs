namespace Shuttle.Abacus.Infrastructure
{
    public interface IPipeline
    {
        void Process<T>(T item);
    }
}
