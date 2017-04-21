using System;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class NewLimitMessage : Message
    {
        public string OwnerName { get; set; }
        public Guid OwnerId { get; set; }

        public NewLimitMessage(string ownerName, Guid ownerId)
        {
            OwnerName = ownerName;
            OwnerId = ownerId;
        }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Method; }
        }
    }
}
