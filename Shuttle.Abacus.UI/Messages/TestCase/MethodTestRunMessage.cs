using System;
using Abacus.Messages;

namespace Abacus.UI
{
    public class MethodTestRunMessage : NullPermissionMessage
    {
        public MethodTestRunMessage(Guid workItemId, MethodTestRunEvent message)
        {
            WorkItemId = workItemId;
            Event = message;
        }

        public Guid WorkItemId { get; set; }
        public MethodTestRunEvent Event { get; set; }
    }
}
