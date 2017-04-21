namespace Abacus.UI
{
    public interface IMethodCoordinator :
        IMessageHandler<PopulateResourceMessage>,
        IMessageHandler<ResourceMenuRequestMessage>,
        IMessageHandler<NewMethodMessage>,
        IMessageHandler<NewMethodFromExistingMessage>,
        IMessageHandler<EditMethodMessage>,
        IMessageHandler<DeleteMethodMessage>,
        IMessageHandler<ResourceRefreshItemTextMessage>,
        IMessageHandler<SummaryViewRequestedMessage>,
        IMessageHandler<ExplorerInitializeMessage>
    {
        
    }
}
