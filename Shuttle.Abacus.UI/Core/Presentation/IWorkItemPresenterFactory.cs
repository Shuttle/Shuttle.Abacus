namespace Shuttle.Abacus.Shell.Core.Presentation
{
    public interface IWorkItemPresenterFactory
    {
        IWorkItemPresenter Create<TWorkItemPresenter>() where TWorkItemPresenter : IWorkItemPresenter;
    }
}
