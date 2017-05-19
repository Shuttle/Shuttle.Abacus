using Shuttle.Abacus.Shell.Core.WorkItem;

namespace Shuttle.Abacus.Shell.Messages.WorkItem
{
    public class WorkItemCompletedMessage : WorkItemMessage
    {
        public WorkItemCompletedMessage(IWorkItem workItem) : base(workItem)
        {
        }
    }
}
