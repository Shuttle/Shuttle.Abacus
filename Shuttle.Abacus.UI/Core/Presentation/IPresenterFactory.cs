namespace Abacus.UI
{
    public interface IPresenterFactory
    {
        TPresenter Create<TPresenter>() where TPresenter : IPresenter;
    }
}
