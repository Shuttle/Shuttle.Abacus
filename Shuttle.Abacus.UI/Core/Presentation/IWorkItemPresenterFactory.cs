namespace Shuttle.Abacus.UI.Core.Presentation
{
    public interface IWorkItemPresenterFactory
    {
        IWorkItemPresenter Create<TWorkItemPresenter>() where TWorkItemPresenter : IWorkItemPresenter;
    }
}
