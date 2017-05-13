using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.TestCase
{
    public class ManageTestsMessage : Message
    {
        public ManageTestsMessage(Guid methodId, string methodName)
        {
            MethodId = methodId;
            MethodName = methodName;
        }

        public override IPermission RequiredPermission => Permissions.Test;

        public Guid MethodId { get; private set; }
        public string MethodName { get; private set; }
    }
}
