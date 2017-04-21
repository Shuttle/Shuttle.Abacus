namespace Abacus.UI
{
    public interface IWorkItemBuilderController
    {
        IWorkItemBuilder ControlledBy<T>() where T : IWorkItemController;
    }

    public interface IWorkItemBuilder
    {
        IWorkItem ShowIn<T>() where T : IWorkItemPresenter;
    }
}
