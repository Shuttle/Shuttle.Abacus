using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.Formula;

namespace Shuttle.Abacus.UI.WorkItemControllers.Interfaces
{
    public interface IFormulaController :
        IWorkItemController,
        IMessageHandler<MoveUpMessage>,
        IMessageHandler<MoveDownMessage>,
        IMessageHandler<NewFormulaMessage>,
        IMessageHandler<EditFormulaMessage>,
        IMessageHandler<DeleteFormulaMessage>
    {
        
    }
}
