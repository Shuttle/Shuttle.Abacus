using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.Test
{
    public class RemoveTestMessage : Message
    {
        public RemoveTestMessage(Guid testId)
        {
            TestId = testId;
        }

        public override IPermission RequiredPermission => Permissions.Test;

        public Guid WorkItemId { get; set; }
        public Guid TestId { get; private set; }
    }
}
