using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.TestCase;

namespace Shuttle.Abacus.UI.WorkItemControllers.Interfaces
{
    public interface ITestController :
        IWorkItemController,
        IMessageHandler<NewTestMessage>,
        IMessageHandler<ChangeTestMessage>
    {

    }
}
