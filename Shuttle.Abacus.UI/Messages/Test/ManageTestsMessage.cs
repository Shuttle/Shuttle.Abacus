using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.Test
{
    public class ManageTestsMessage : Message
    {
        public ManageTestsMessage(Guid methodId, string methodName)
        {
            MethodName = methodName;
        }

        public override IPermission RequiredPermission => Permissions.Test;

        public string MethodName { get; private set; }
    }
}
