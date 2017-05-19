using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.WorkItem;
using Shuttle.Abacus.Shell.Messages.Argument;

namespace Shuttle.Abacus.Shell.UI.Argument
{
    public interface IArgumentController : 
        IWorkItemController, 
        IMessageHandler<RegisterArgumentMessage>,
        IMessageHandler<RenameArgumentMessage>,
        IMessageHandler<RemoveArgumentMessage>
    {

    }
}
