namespace Abacus.UI
{
    public interface IDecimalTableCoordinator :
        IMessageHandler<ExplorerInitializeMessage>,
        IMessageHandler<ResourceMenuRequestMessage>,
        IMessageHandler<PopulateResourceMessage>,
        IMessageHandler<ResourceRefreshItemTextMessage>,
        IMessageHandler<NewDecimalTableMessage>,
        IMessageHandler<EditDecimalTableMessage>,
        IMessageHandler<NewDecimalTableFromExistingMessage>,
        IMessageHandler<SummaryViewRequestedMessage>
    {
        
    }
}
