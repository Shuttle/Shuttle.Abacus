namespace Abacus.UI
{
    public abstract class WorkItemMessage : NullPermissionMessage
    {
        protected WorkItemMessage(IWorkItem workItem)
        {
            WorkItem = workItem;
        }

        public IWorkItem WorkItem { get; private set; }
    }
}
