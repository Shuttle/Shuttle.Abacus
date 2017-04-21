namespace Abacus.UI
{
    public interface ISystemUserListController : 
        IWorkItemController,
        IMessageHandler<DoubleClickMessage>,
        IMessageHandler<EditLoginNameMessage>,
        IMessageHandler<EditPermissionsMessage>
    {
        
    }
}
