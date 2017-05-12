using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.Formula;

namespace Shuttle.Abacus.UI.WorkItemControllers.Interfaces
{
    public interface IFormulaController :
        IWorkItemController,
        IMessageHandler<RegisterFormulaMessage>,
        IMessageHandler<RenameFormulaMessage>,
        IMessageHandler<RemoveFormulaMessage>
    {
        
    }
}
