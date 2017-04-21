namespace Abacus.Application
{
    public interface ITaskFactory
    {
        T Create<T>();
    }
}
