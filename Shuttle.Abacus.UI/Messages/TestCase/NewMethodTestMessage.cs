using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.TestCase
{
    public class NewMethodTestMessage : Message
    {
        public NewMethodTestMessage(Guid methodId)
        {
            MethodId = methodId;
        }

        public NewMethodTestMessage(NewMethodTestFromExistingMessage message)
        {
            MethodId = message.MethodId;
            WorkItemId = message.WorkItemId;
        }

        public Guid MethodId { get; private set; }
        public Guid WorkItemId { get; set; }

        public override IPermission RequiredPermission
        {
            get { return Permissions.MethodTest; }
        }

    }
}
