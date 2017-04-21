namespace Shuttle.Abacus.ApplicationService
{
    public interface ITaskFactory
    {
        T Create<T>();
    }
}
