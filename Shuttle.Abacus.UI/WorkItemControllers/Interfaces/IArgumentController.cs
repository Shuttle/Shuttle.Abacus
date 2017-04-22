using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.FactorAnswer;

namespace Shuttle.Abacus.UI.WorkItemControllers.Interfaces
{
    public interface IArgumentController : 
        IWorkItemController, 
        IMessageHandler<NewArgumentMessage>,
        IMessageHandler<EditArgumentMessage>,
        IMessageHandler<DeleteArgumentMessage>
    {

    }
}
