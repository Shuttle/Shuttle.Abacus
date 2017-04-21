namespace Abacus.UI
{
    public interface ISystemUserController :
        IWorkItemController,
        IMessageHandler<NewSystemUserMessage>,
        IMessageHandler<EditLoginNameMessage>,
        IMessageHandler<EditPermissionsMessage>
    {
    }
}
