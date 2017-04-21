namespace Abacus.UI
{
    public interface IArgumentController : 
        IWorkItemController, 
        IMessageHandler<NewArgumentMessage>,
        IMessageHandler<EditArgumentMessage>,
        IMessageHandler<DeleteArgumentMessage>
    {

    }
}
