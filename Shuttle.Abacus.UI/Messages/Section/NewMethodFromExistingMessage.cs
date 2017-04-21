using System;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class NewMethodFromExistingMessage : Message
    {
        public Guid MethodId { get; private set; }

        public NewMethodFromExistingMessage(Guid methodId, string methodName)
        {
            MethodId = methodId;
            MethodName = methodName;
        }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Method; }
        }

        public string MethodName { get; private set; }
    }
}
