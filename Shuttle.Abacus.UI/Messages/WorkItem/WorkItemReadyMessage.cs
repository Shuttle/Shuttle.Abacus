namespace Abacus.UI
{
    public class WorkItemReadyMessage : WorkItemMessage
    {
        public WorkItemReadyMessage(IWorkItem workItem) : base(workItem)
        {
        }
    }
}
