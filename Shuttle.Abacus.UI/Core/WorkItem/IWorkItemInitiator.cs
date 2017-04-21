using System;

namespace Abacus.UI
{
    public interface IWorkItemInitiator
    {
        Guid WorkItemInitiatorId { get; }
        bool RefreshOwner { get; }
        
        IWorkItemInitiator WithRefreshOwner();

        string ToolTipText { get; set; }
    }
}
