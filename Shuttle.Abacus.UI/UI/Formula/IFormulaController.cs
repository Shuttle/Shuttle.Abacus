using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.Formula;

namespace Shuttle.Abacus.UI.UI.Formula
{
    public interface IFormulaController :
        IWorkItemController,
        IMessageHandler<RegisterFormulaMessage>,
        IMessageHandler<RenameFormulaMessage>,
        IMessageHandler<RemoveFormulaMessage>,
        IMessageHandler<ManageFormulaOperationsMessage>,
        IMessageHandler<ManageFormulaConstraintsMessage>
    {

    }
}
