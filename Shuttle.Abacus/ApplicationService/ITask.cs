namespace Abacus.Application
{
    public interface ITask<T>
    {
        void Execute(T item);
    }
}
