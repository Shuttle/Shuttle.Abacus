using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.SystemUser;

namespace Shuttle.Abacus.UI.UI.SystemUser
{
    public interface ISystemUserController :
        IWorkItemController,
        IMessageHandler<NewSystemUserMessage>,
        IMessageHandler<EditLoginNameMessage>,
        IMessageHandler<EditPermissionsMessage>
    {
    }
}
