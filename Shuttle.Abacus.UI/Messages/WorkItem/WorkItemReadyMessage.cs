using Shuttle.Abacus.Shell.Core.WorkItem;

namespace Shuttle.Abacus.Shell.Messages.WorkItem
{
    public class WorkItemReadyMessage : WorkItemMessage
    {
        public WorkItemReadyMessage(IWorkItem workItem) : base(workItem)
        {
        }
    }
}
