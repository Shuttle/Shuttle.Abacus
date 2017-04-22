using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.Calculation;
using Shuttle.Abacus.UI.Messages.Core;

namespace Shuttle.Abacus.UI.WorkItemControllers.Interfaces
{
    public interface ICalculationController :
        IWorkItemController,
        IMessageHandler<NewCalculationMessage>,
        IMessageHandler<EditCalculationMessage>,
        IMessageHandler<DeleteCalculationMessage>,
        IMessageHandler<GrabCalculationsMessage>,
        IMessageHandler<MoveUpMessage>,
        IMessageHandler<MoveDownMessage>,
        IMessageHandler<ChangeCalculationOrderMessage>,
        IMessageHandler<ManageCalculationConstraintsMessage>
    {
    }
}
