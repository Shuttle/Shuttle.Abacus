using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.Test
{
    public class TestCreatedMessage : Message
    {
        public TestCreatedMessage(Guid workItemId, Guid methodId)
        {
            WorkItemId = workItemId;
            MethodId = methodId;
        }

        public Guid WorkItemId { get; set; }

        public Guid MethodId { get; set; }

        public override IPermission RequiredPermission => Permissions.Test;
    }
}
