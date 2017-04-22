using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.Core;

namespace Shuttle.Abacus.UI.Messages.WorkItem
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
