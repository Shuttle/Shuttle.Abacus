namespace Abacus.UI
{
    public interface IContextToolbarPresenter : IWorkItemPresenter
    {
        void Close();
        void InvokeMessage(Message message);
        void PresenterSelected(IPresenter presenter);
        void NoPresenterSelected();
    }
}
