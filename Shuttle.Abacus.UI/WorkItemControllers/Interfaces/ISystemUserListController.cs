using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.SystemUser;

namespace Shuttle.Abacus.UI.WorkItemControllers.Interfaces
{
    public interface ISystemUserListController : 
        IWorkItemController,
        IMessageHandler<DoubleClickMessage>,
        IMessageHandler<EditLoginNameMessage>,
        IMessageHandler<EditPermissionsMessage>
    {
        
    }
}
