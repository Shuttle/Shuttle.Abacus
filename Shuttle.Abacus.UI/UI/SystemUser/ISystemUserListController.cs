using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.WorkItem;
using Shuttle.Abacus.Shell.Messages.Core;
using Shuttle.Abacus.Shell.Messages.SystemUser;

namespace Shuttle.Abacus.Shell.UI.SystemUser
{
    public interface ISystemUserListController : 
        IWorkItemController,
        IMessageHandler<DoubleClickMessage>,
        IMessageHandler<EditLoginNameMessage>,
        IMessageHandler<EditPermissionsMessage>
    {
        
    }
}
