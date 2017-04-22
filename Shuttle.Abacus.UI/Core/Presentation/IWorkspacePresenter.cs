using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.WorkItem;

namespace Shuttle.Abacus.UI.Core.Presentation
{
    public interface IWorkspacePresenter : 
        IPresenter,
        IMessageHandler<WorkItemCompletedMessage>,
        IMessageHandler<WorkItemBusyMessage>,
        IMessageHandler<WorkItemReadyMessage>,
        IMessageHandler<WorkItemTextChangedMessage>,
        IMessageHandler<ShowPresenterMessage>
    {
        IWorkspacePresenter Add(IWorkItem workItem);
    }
}
