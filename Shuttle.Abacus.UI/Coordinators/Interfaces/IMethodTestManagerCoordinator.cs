namespace Abacus.UI
{
    public interface IMethodTestManagerCoordinator :
        ICoordinator,
        IMessageHandler<ResourceMenuRequestMessage>,
        IMessageHandler<ManageMethodTestsMessage>,
        IMessageHandler<NewMethodTestMessage>,
        IMessageHandler<NewMethodTestFromExistingMessage>,
        IMessageHandler<EditMethodTestMessage>,
        IMessageHandler<MethodTestCreatedMessage>,
        IMessageHandler<MethodTestChangedMessage>,
        IMessageHandler<MethodTestRemovedMessage>,
        IMessageHandler<MethodTestRunMessage>,
        IMessageHandler<SummaryViewRequestedMessage>
    {

    }
}
