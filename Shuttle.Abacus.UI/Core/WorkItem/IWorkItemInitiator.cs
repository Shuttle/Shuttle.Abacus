using System;

namespace Shuttle.Abacus.Shell.Core.WorkItem
{
    public interface IWorkItemInitiator
    {
        Guid WorkItemInitiatorId { get; }
        bool RefreshOwner { get; }
        
        IWorkItemInitiator WithRefreshOwner();

        string ToolTipText { get; set; }
    }
}
