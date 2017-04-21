using Abacus.Infrastructure;

namespace Abacus.Application
{
    public class TaskFactory : ITaskFactory
    {
        public T Create<T>()
        {
            return DependencyResolver.Resolve<T>();
        }
    }
}
