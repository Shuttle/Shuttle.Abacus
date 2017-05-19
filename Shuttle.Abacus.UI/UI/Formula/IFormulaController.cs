using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.WorkItem;
using Shuttle.Abacus.Shell.Messages.Formula;

namespace Shuttle.Abacus.Shell.UI.Formula
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
