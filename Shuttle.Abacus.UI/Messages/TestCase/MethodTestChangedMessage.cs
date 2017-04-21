using System;

namespace Abacus.UI
{
    public class MethodTestChangedMessage : NullPermissionMessage
    {
        public MethodTestChangedMessage(Guid workItemId, Guid methodId)
        {
            WorkItemId = workItemId;
            MethodId = methodId;
        }

        public Guid WorkItemId { get; private set; }
        public Guid MethodId { get; private set; }
    }
}
