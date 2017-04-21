using System;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class EditMethodMessage : Message
    {
        public Guid MethodId;

        public EditMethodMessage(Guid methodId)
        {
            MethodId = methodId;
        }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Method; }
        }
    }
}
