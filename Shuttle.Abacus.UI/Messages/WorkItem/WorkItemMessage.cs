using Shuttle.Abacus.Shell.Core.WorkItem;
using Shuttle.Abacus.Shell.Messages.Core;

namespace Shuttle.Abacus.Shell.Messages.WorkItem
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
