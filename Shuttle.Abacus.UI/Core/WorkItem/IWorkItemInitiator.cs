using System;

namespace Shuttle.Abacus.UI.Core.WorkItem
{
    public interface IWorkItemInitiator
    {
        Guid WorkItemInitiatorId { get; }
        bool RefreshOwner { get; }
        
        IWorkItemInitiator WithRefreshOwner();

        string ToolTipText { get; set; }
    }
}
