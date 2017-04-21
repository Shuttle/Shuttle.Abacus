namespace Abacus.UI
{
    public interface IFormulaController :
        IWorkItemController,
        IMessageHandler<MoveUpMessage>,
        IMessageHandler<MoveDownMessage>,
        IMessageHandler<NewFormulaMessage>,
        IMessageHandler<EditFormulaMessage>,
        IMessageHandler<DeleteFormulaMessage>,
        IMessageHandler<ChangeFormulaOrderMessage>
    {
        
    }
}
