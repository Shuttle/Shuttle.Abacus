using Shuttle.Abacus.Shell.Core.WorkItem;
using Shuttle.Abacus.Shell.Messages.Core;

namespace Shuttle.Abacus.Shell.Messages.WorkItem
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
