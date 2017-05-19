using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.WorkItem;
using Shuttle.Abacus.Shell.Messages.Core;
using Shuttle.Abacus.Shell.Messages.WorkItem;

namespace Shuttle.Abacus.Shell.Core.Presentation
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
