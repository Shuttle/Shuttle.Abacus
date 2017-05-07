using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.Section
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
            get { return Permissions.Formula; }
        }

        public string MethodName { get; private set; }
    }
}
