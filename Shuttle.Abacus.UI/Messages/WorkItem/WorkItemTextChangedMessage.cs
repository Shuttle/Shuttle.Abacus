using Shuttle.Abacus.Shell.Core.WorkItem;

namespace Shuttle.Abacus.Shell.Messages.WorkItem
{
    public class WorkItemTextChangedMessage : WorkItemMessage
    {
        public WorkItemTextChangedMessage(IWorkItem workItem) : base(workItem)
        {
        }
    }
}
