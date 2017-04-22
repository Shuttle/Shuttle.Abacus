using System;
using Shuttle.Abacus.UI.Messages.Core;

namespace Shuttle.Abacus.UI.Messages.WorkItem
{
    public class RefreshWorkItemDispatcherTextMessage : NullPermissionMessage
    {
        public RefreshWorkItemDispatcherTextMessage(Guid dispatchedMessageId)
        {
            DispatchedMessageId = dispatchedMessageId;
        }

        public Guid DispatchedMessageId { get; private set; }
        
    }
}
