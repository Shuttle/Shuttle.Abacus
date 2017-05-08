using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.DecimalTable;

namespace Shuttle.Abacus.UI.WorkItemControllers.Interfaces
{
    public interface IMatrixController :
        IWorkItemController,
        IMessageHandler<NewDecimalTableMessage>,
        IMessageHandler<EditDecimalTableMessage>
    {
        
    }
}
