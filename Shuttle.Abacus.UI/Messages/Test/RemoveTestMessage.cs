using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.TestCase
{
    public class RemoveTestMessage : Message
    {
        public RemoveTestMessage(Guid methodId)
        {
            MethodId = methodId;
        }

        public override IPermission RequiredPermission => Permissions.Test;

        public Guid WorkItemId { get; set; }
        public Guid MethodId { get; private set; }
    }
}
