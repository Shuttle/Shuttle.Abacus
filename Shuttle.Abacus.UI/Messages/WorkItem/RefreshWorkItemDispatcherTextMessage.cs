using System;

namespace Abacus.UI
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
