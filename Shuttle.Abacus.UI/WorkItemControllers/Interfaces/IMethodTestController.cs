namespace Abacus.UI
{
    public interface IMethodTestController :
        IWorkItemController,
        IMessageHandler<NewMethodTestMessage>,
        IMessageHandler<ChangeMethodTestMessage>
    {

    }
}
