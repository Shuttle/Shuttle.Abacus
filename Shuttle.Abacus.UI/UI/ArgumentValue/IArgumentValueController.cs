using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.WorkItem;
using Shuttle.Abacus.Shell.Messages.ArgumentValue;

namespace Shuttle.Abacus.Shell.UI.ArgumentValue
{
    public interface IArgumentValueController : 
        IWorkItemController, 
        IMessageHandler<RegisterArgumentValueMessage>,
        IMessageHandler<RemoveArgumentValueMessage>
    {

    }
}
