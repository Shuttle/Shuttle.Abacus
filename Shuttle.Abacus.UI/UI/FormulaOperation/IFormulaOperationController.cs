using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.Formula;

namespace Shuttle.Abacus.UI.UI.FormulaOperation
{
    public interface IFormulaOperationController :
        IWorkItemController,
        IMessageHandler<MoveUpMessage>,
        IMessageHandler<MoveDownMessage>,
        IMessageHandler<ManageFormulaOperationsMessage>
    {
        
    }
}
