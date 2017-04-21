namespace Abacus.UI
{
    public class RefreshWorkItemDispatcherMessage : NullPermissionMessage
    {
        public IWorkItem WorkItem { get; private set; }

        public RefreshWorkItemDispatcherMessage(IWorkItem workItem)
        {
            WorkItem = workItem;
        }
    }
}
