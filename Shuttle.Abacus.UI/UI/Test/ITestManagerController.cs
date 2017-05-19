using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.WorkItem;
using Shuttle.Abacus.Shell.Messages.Core;
using Shuttle.Abacus.Shell.Messages.Test;

namespace Shuttle.Abacus.Shell.UI.Test
{
    public interface ITestManagerController :
        IWorkItemController,
        IMessageHandler<RegisterTestMessage>,
        IMessageHandler<EditTestMessage>,
        IMessageHandler<RemoveTestMessage>,
        IMessageHandler<RunTestMessage>,
        IMessageHandler<ListReadyMessage>,
        IMessageHandler<MarkAllMessage>,
        IMessageHandler<InvertMarksMessage>,
        IMessageHandler<PrintTestMessage>
    {
    }
}
