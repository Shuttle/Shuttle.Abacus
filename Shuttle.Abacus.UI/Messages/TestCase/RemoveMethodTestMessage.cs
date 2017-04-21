using System;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class RemoveMethodTestMessage : Message
    {
        public RemoveMethodTestMessage(Guid methodId)
        {
            MethodId = methodId;
        }

        public override IPermission RequiredPermission
        {
            get { return Permissions.MethodTest; }
        }

        public Guid WorkItemId { get; set; }
        public Guid MethodId { get; private set; }
    }
}
