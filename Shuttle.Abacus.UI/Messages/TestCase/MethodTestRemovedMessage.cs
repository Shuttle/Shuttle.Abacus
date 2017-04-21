using System;

namespace Abacus.UI
{
    public class MethodTestRemovedMessage : NullPermissionMessage
    {
        public MethodTestRemovedMessage(Guid workItemId, Guid methodId)
        {
            WorkItemId = workItemId;
            MethodId = methodId;
        }

        public Guid WorkItemId { get; private set; }
        public Guid MethodId { get; private set; }
    }
}
