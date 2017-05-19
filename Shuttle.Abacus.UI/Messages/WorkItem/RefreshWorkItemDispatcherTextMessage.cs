using System;
using Shuttle.Abacus.Shell.Messages.Core;

namespace Shuttle.Abacus.Shell.Messages.WorkItem
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
