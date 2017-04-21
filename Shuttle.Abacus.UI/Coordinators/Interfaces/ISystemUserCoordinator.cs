using Abacus.Data;

namespace Abacus.UI
{
    public interface ISystemUserCoordinator :
        ICoordinator,
        IMessageHandler<ListSystemUserMessage>,
        IMessageHandler<NewSystemUserMessage>,
        IMessageHandler<EditLoginNameMessage>,
        IMessageHandler<EditPermissionsMessage>
    {
        ISystemUserQuery SystemUserQuery { get; set; }
    }
}
