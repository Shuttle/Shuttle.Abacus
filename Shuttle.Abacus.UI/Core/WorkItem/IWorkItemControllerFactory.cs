namespace Shuttle.Abacus.Shell.Core.WorkItem
{
    public interface IWorkItemControllerFactory
    {
        T Create<T>() where T : IWorkItemController;
    }
}
