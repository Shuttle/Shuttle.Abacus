namespace Abacus.UI
{
    public interface IFormulaCoordinator :
        IMessageHandler<ResourceMenuRequestMessage>,
        IMessageHandler<PopulateResourceMessage>,
        IMessageHandler<NewFormulaMessage>,
        IMessageHandler<NewFormulaFromExistingMessage>,
        IMessageHandler<EditFormulaMessage>,
        IMessageHandler<DeleteFormulaMessage>,
        IMessageHandler<ResourceRefreshItemTextMessage>,
        IMessageHandler<ChangeFormulaOrderMessage>,
        IMessageHandler<SummaryViewRequestedMessage>
    {
    }
}
