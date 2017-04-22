namespace Shuttle.Abacus.UI.Core.WorkItem
{
    public class WorkItemControllerFactory : IWorkItemControllerFactory
    {
        public T Create<T>() where T : IWorkItemController
        {
            return DependencyResolver.Resolve<T>();
        }
    }
}
