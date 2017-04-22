using System;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.TestCase
{
    public class ManageMethodTestsMessage : Message
    {
        public ManageMethodTestsMessage(Guid methodId, string methodName)
        {
            MethodId = methodId;
            MethodName = methodName;
        }

        public override IPermission RequiredPermission
        {
            get { return Permissions.MethodTest; }
        }

        public Guid MethodId { get; private set; }
        public string MethodName { get; private set; }
    }
}
