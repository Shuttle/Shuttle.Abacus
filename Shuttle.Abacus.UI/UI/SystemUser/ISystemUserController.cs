using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.WorkItem;
using Shuttle.Abacus.Shell.Messages.SystemUser;

namespace Shuttle.Abacus.Shell.UI.SystemUser
{
    public interface ISystemUserController :
        IWorkItemController,
        IMessageHandler<NewSystemUserMessage>,
        IMessageHandler<EditLoginNameMessage>,
        IMessageHandler<EditPermissionsMessage>
    {
    }
}
