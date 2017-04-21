namespace Abacus.UI
{
    public interface ISystemUserPresenter :
        IPresenter,
        IMessageHandler<EditLoginNameMessage>
    {
        void LoginNameExited();
    }
}
