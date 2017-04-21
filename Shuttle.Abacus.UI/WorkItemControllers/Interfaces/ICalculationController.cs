namespace Abacus.UI
{
    public interface ICalculationController :
        IWorkItemController,
        IMessageHandler<NewCalculationMessage>,
        IMessageHandler<EditCalculationMessage>,
        IMessageHandler<DeleteCalculationMessage>,
        IMessageHandler<GrabCalculationsMessage>,
        IMessageHandler<MoveUpMessage>,
        IMessageHandler<MoveDownMessage>,
        IMessageHandler<ChangeCalculationOrderMessage>,
        IMessageHandler<ManageCalculationConstraintsMessage>
    {
    }
}
