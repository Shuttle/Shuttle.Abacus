using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.TestCase
{
    public class NewMethodTestFromExistingMessage : Message
    {
        public NewMethodTestFromExistingMessage(Guid methodId)
        {
            MethodId = methodId;
        }

        public Guid MethodId { get; private set; }
        public Guid MethodTestId { get; set; }
        public Guid WorkItemId { get; set; }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Test; }
        }
    }
}
