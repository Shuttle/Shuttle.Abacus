using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.WorkItem;
using Shuttle.Abacus.Shell.Messages.DecimalTable;

namespace Shuttle.Abacus.Shell.UI.Matrix
{
    public interface IMatrixController :
        IWorkItemController,
        IMessageHandler<NewMatrixMessage>,
        IMessageHandler<EditMatrixMessage>
    {
        
    }
}
