using System;
using Abacus.Infrastructure;

namespace Abacus.UI
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
