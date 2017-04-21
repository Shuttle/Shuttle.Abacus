namespace Abacus.UI
{
    public interface IDecimalTableController :
        IWorkItemController,
        IMessageHandler<NewDecimalTableMessage>,
        IMessageHandler<EditDecimalTableMessage>
    {
        
    }
}
