using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.SystemUser;

namespace Shuttle.Abacus.UI.WorkItemControllers.Interfaces
{
    public interface ISystemUserController :
        IWorkItemController,
        IMessageHandler<NewSystemUserMessage>,
        IMessageHandler<EditLoginNameMessage>,
        IMessageHandler<EditPermissionsMessage>
    {
    }
}
