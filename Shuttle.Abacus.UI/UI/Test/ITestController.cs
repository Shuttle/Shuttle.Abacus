using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.TestCase;

namespace Shuttle.Abacus.UI.UI.Test
{
    public interface ITestController :
        IWorkItemController,
        IMessageHandler<NewTestMessage>,
        IMessageHandler<ChangeTestMessage>
    {

    }
}
