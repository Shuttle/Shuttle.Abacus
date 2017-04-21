namespace Shuttle.Abacus.ApplicationService
{
    public interface ITask<T>
    {
        void Execute(T item);
    }
}
