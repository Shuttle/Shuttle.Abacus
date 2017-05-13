using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.TestCase
{
    public class NewTestMessage : Message
    {
        public NewTestMessage(Guid methodId)
        {
            MethodId = methodId;
        }

        public NewTestMessage(NewTestFromExistingMessage message)
        {
            MethodId = message.MethodId;
            WorkItemId = message.WorkItemId;
        }

        public Guid MethodId { get; private set; }
        public Guid WorkItemId { get; set; }

        public override IPermission RequiredPermission => Permissions.Test;
    }
}
