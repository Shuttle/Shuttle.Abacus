namespace Abacus.UI
{
    public interface ILimitController :
        IWorkItemController,
        IMessageHandler<NewLimitMessage>,
        IMessageHandler<EditLimitMessage>,
        IMessageHandler<DeleteLimitMessage>
    {
        
    }
}
