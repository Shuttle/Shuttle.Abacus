using System;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.TestCase
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
