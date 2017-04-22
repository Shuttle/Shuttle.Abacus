namespace Shuttle.Abacus.UI.Core.WorkItem
{
    public interface IWorkItemControllerFactory
    {
        T Create<T>() where T : IWorkItemController;
    }
}
