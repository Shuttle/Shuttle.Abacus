namespace Abacus.UI
{
    public class WorkItemTextChangedMessage : WorkItemMessage
    {
        public WorkItemTextChangedMessage(IWorkItem workItem) : base(workItem)
        {
        }
    }
}
