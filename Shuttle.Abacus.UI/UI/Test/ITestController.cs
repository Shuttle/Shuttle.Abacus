using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.Test;

namespace Shuttle.Abacus.UI.UI.Test
{
    public interface ITestController :
        IWorkItemController,
        IMessageHandler<RegisterTestMessage>,
        IMessageHandler<ChangeTestMessage>
    {

    }
}
