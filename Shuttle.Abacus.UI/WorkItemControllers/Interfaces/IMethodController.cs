namespace Abacus.UI
{
    public interface IMethodController :
        IWorkItemController,
        IMessageHandler<NewMethodMessage>,
        IMessageHandler<NewMethodFromExistingMessage>,
        IMessageHandler<EditMethodMessage>,
        IMessageHandler<DeleteMethodMessage>
    {

    }
}
