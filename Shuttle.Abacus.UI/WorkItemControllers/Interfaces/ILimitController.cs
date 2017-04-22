using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.Limit;

namespace Shuttle.Abacus.UI.WorkItemControllers.Interfaces
{
    public interface ILimitController :
        IWorkItemController,
        IMessageHandler<NewLimitMessage>,
        IMessageHandler<EditLimitMessage>,
        IMessageHandler<DeleteLimitMessage>
    {
        
    }
}
