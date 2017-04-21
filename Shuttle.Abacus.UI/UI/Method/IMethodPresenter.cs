namespace Abacus.UI
{
    public interface IMethodPresenter :
        IPresenter
    {
        void TitleExited();

        void HandleMessage(EditMethodMessage message);
    }
}
