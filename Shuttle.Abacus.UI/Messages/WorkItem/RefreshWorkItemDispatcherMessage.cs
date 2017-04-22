using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.Core;

namespace Shuttle.Abacus.UI.Messages.WorkItem
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
