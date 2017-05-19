using Shuttle.Abacus.Shell.Core.WorkItem;

namespace Shuttle.Abacus.Shell.Messages.WorkItem
{
    public class WorkItemBusyMessage : WorkItemMessage
    {
        public WorkItemBusyMessage(IWorkItem workItem) : base(workItem)
        {
        }
    }
}
