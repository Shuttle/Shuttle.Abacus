using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.Argument;

namespace Shuttle.Abacus.UI.UI.Argument
{
    public interface IArgumentController : 
        IWorkItemController, 
        IMessageHandler<RegisterArgumentMessage>,
        IMessageHandler<RenameArgumentMessage>,
        IMessageHandler<RemoveArgumentMessage>
    {

    }
}
