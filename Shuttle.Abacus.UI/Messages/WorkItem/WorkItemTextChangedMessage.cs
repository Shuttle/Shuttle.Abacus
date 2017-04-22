using Shuttle.Abacus.UI.Core.WorkItem;

namespace Shuttle.Abacus.UI.Messages.WorkItem
{
    public class WorkItemTextChangedMessage : WorkItemMessage
    {
        public WorkItemTextChangedMessage(IWorkItem workItem) : base(workItem)
        {
        }
    }
}
