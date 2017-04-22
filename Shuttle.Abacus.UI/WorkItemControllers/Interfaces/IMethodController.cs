using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.Section;

namespace Shuttle.Abacus.UI.WorkItemControllers.Interfaces
{
    public interface IMethodController :
        IWorkItemController,
        IMessageHandler<NewMethodMessage>,
        IMessageHandler<NewMethodFromExistingMessage>,
        IMessageHandler<EditMethodMessage>,
        IMessageHandler<DeleteMethodMessage>
    {

    }
}
