namespace Shuttle.Abacus.Shell.Core.Presentation
{
    public interface IPresenterFactory
    {
        TPresenter Create<TPresenter>() where TPresenter : IPresenter;
    }
}
