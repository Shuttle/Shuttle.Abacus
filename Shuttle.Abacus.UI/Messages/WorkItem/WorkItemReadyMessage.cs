using Shuttle.Abacus.UI.Core.WorkItem;

namespace Shuttle.Abacus.UI.Messages.WorkItem
{
    public class WorkItemReadyMessage : WorkItemMessage
    {
        public WorkItemReadyMessage(IWorkItem workItem) : base(workItem)
        {
        }
    }
}
