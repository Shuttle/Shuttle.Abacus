using System;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class ChangeCalculationOrderMessage : Message
    {
        public ChangeCalculationOrderMessage(Guid methodId, Guid ownerId, string ownerText)
        {
            MethodId = methodId;
            OwnerId = ownerId;
            OwnerText = ownerText;
        }

        public Guid MethodId { get; private set; }
        public Guid OwnerId { get; private set; }
        public string OwnerText { get; private set; }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Method; }
        }
    }
}
