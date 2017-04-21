using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.ApplicationService
{
    public class TaskFactory : ITaskFactory
    {
        public T Create<T>()
        {
            return DependencyResolver.Resolve<T>();
        }
    }
}
