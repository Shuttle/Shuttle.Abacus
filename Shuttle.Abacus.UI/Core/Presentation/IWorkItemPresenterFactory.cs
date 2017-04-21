namespace Abacus.UI
{
    public interface IWorkItemPresenterFactory
    {
        IWorkItemPresenter Create<TWorkItemPresenter>() where TWorkItemPresenter : IWorkItemPresenter;
    }
}
