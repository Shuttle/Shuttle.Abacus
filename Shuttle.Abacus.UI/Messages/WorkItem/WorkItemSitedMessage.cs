namespace Abacus.UI
{
    public class WorkItemSitedMessage : NullPermissionMessage
    {
        public WorkItemSitedMessage(IWorkItem workItem)
        {
            WorkItem = workItem;
        }

        public IWorkItem WorkItem { get; private set; }
    }
}
