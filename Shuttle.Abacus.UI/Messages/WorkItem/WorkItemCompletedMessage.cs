using Shuttle.Abacus.UI.Core.WorkItem;

namespace Shuttle.Abacus.UI.Messages.WorkItem
{
    public class WorkItemCompletedMessage : WorkItemMessage
    {
        public WorkItemCompletedMessage(IWorkItem workItem) : base(workItem)
        {
        }
    }
}
