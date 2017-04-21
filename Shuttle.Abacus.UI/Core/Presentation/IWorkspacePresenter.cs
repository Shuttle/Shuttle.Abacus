namespace Abacus.UI
{
    public interface IWorkspacePresenter : 
        IPresenter,
        IMessageHandler<WorkItemCompletedMessage>,
        IMessageHandler<WorkItemBusyMessage>,
        IMessageHandler<WorkItemReadyMessage>,
        IMessageHandler<WorkItemTextChangedMessage>,
        IMessageHandler<ShowPresenterMessage>
    {
        IWorkspacePresenter Add(IWorkItem workItem);
    }
}
