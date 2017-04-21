using System;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class NewCalculationMessage : Message
    {
        public NewCalculationMessage(Guid methodId, string ownerName, Guid ownerId)
        {
            MethodId = methodId;
            OwnerName = ownerName;
            OwnerId = ownerId;
        }

        public Guid MethodId { get; set; }
        public Guid OwnerId { get; set; }
        public string OwnerName { get; set; }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Method; }
        }
    }
}
