namespace Abacus.UI
{
    public interface IWorkItemPresenter : 
        IPresenter,
        IMessageHandler<ShowPresenterMessage>
    {
        void AddPresenter(IPresenter presenter);
        void SelectPresenter(IPresenter presenter);
        
        void DisableMessage<T>();
        void EnableMessage<T>();
        bool IsMessageEnabled(Message message);
        void ResetChanges();
    }
}
