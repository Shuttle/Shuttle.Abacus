using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class WorkItemControllerFactory : IWorkItemControllerFactory
    {
        public T Create<T>() where T : IWorkItemController
        {
            return DependencyResolver.Resolve<T>();
        }
    }
}
