namespace Abacus.UI
{
    public interface ILimitCoordinator :
        IMessageHandler<PopulateResourceMessage>,
        IMessageHandler<ResourceMenuRequestMessage>,
        IMessageHandler<NewLimitMessage>,
        IMessageHandler<EditLimitMessage>,
        IMessageHandler<DeleteLimitMessage>,
        IMessageHandler<SummaryViewRequestedMessage>
    {
        
    }
}
