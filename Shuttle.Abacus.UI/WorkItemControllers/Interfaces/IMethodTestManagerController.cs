namespace Abacus.UI
{
    public interface IMethodTestManagerController :
        IWorkItemController,
        IMessageHandler<NewMethodTestMessage>,
        IMessageHandler<NewMethodTestFromExistingMessage>,
        IMessageHandler<EditMethodTestMessage>,
        IMessageHandler<RemoveMethodTestMessage>,
        IMessageHandler<RunMethodTestMessage>,
        IMessageHandler<ListReadyMessage>,
        IMessageHandler<MarkAllMessage>,
        IMessageHandler<InvertMarksMessage>,
        IMessageHandler<PrintMethodTestMessage>
    {
    }
}
