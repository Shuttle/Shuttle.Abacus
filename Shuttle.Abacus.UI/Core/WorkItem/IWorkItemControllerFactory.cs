namespace Abacus.UI
{
    public interface IWorkItemControllerFactory
    {
        T Create<T>() where T : IWorkItemController;
    }
}
