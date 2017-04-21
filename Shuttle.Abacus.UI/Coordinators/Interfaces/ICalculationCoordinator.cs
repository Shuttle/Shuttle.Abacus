namespace Abacus.UI
{
    public interface ICalculationCoordinator :
        IMessageHandler<PopulateResourceMessage>,
        IMessageHandler<ResourceMenuRequestMessage>,
        IMessageHandler<ChangeCalculationOrderMessage>,
        IMessageHandler<NewCalculationMessage>,
        IMessageHandler<EditCalculationMessage>,
        IMessageHandler<DeleteCalculationMessage>,
        IMessageHandler<GrabCalculationsMessage>,
        IMessageHandler<ResourceRefreshItemTextMessage>,
        IMessageHandler<SummaryViewRequestedMessage>
    {
        
    }
}
