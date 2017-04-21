namespace Abacus.UI
{
    public interface IArgumentCoordinator :
        IMessageHandler<ExplorerInitializeMessage>,
        IMessageHandler<ResourceMenuRequestMessage>,
        IMessageHandler<PopulateResourceMessage>,
        IMessageHandler<NewArgumentMessage>, 
        IMessageHandler<EditArgumentMessage>,
        IMessageHandler<DeleteArgumentMessage>,
        IMessageHandler<ResourceRefreshItemTextMessage>,
        IMessageHandler<SummaryViewRequestedMessage>
    {

    }
}
