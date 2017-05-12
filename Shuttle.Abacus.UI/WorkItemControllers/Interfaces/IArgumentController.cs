using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.Argument;
using Shuttle.Abacus.UI.Messages.FactorAnswer;

namespace Shuttle.Abacus.UI.WorkItemControllers.Interfaces
{
    public interface IArgumentController : 
        IWorkItemController, 
        IMessageHandler<RegisterArgumentMessage>,
        IMessageHandler<RenameArgumentMessage>,
        IMessageHandler<RemoveArgumentMessage>
    {

    }
}
