using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.ArgumentValue;

namespace Shuttle.Abacus.UI.UI.ArgumentValue
{
    public interface IArgumentValueController : 
        IWorkItemController, 
        IMessageHandler<RegisterArgumentValueMessage>,
        IMessageHandler<RemoveArgumentValueMessage>
    {

    }
}
