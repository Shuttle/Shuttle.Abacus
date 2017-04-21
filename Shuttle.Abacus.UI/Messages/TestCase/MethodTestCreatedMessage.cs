using System;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class MethodTestCreatedMessage : Message
    {
        public MethodTestCreatedMessage(Guid workItemId, Guid methodId)
        {
            WorkItemId = workItemId;
            MethodId = methodId;
        }

        public Guid WorkItemId { get; set; }

        public Guid MethodId { get; set; }

        public override IPermission RequiredPermission
        {
            get { return Permissions.MethodTest; }
        }
    }
}
