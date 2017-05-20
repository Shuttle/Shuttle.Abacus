using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.WorkItem;
using Shuttle.Abacus.Shell.Messages.Test;

namespace Shuttle.Abacus.Shell.UI.Test
{
    public interface ITestController :
        IWorkItemController,
        IMessageHandler<RegisterTestMessage>,
        IMessageHandler<RegisterTestArgumentMessage>,
        IMessageHandler<RemoveTestArgumentMessage>,
        IMessageHandler<RunTestMessage>
    {
    }
}
