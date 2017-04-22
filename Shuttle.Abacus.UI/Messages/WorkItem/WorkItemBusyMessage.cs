using Shuttle.Abacus.UI.Core.WorkItem;

namespace Shuttle.Abacus.UI.Messages.WorkItem
{
    public class WorkItemBusyMessage : WorkItemMessage
    {
        public WorkItemBusyMessage(IWorkItem workItem) : base(workItem)
        {
        }
    }
}
