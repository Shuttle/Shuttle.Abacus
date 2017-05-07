using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.TestCase
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
            get { return Permissions.Test; }
        }
    }
}
