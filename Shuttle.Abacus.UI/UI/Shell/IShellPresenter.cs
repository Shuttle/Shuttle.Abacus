namespace Abacus.UI
{
    public interface IShellPresenter :
        IPresenter,
        IMessageHandler<StartShellMessage>,
        IMessageHandler<ApplicationExitMessage>,
        IMessageHandler<WorkStartedMessage>,
        IMessageHandler<WorkCompletedMessage>,
        IMessageHandler<StatusMessage>,
        IMessageHandler<ReadyStatusMessage>
    {
        void PublishMessage(Message message);
        void Configure();
    }
}
