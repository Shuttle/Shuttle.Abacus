namespace Shuttle.Abacus.UI.Core.Presentation
{
    public interface IPresenterFactory
    {
        TPresenter Create<TPresenter>() where TPresenter : IPresenter;
    }
}
