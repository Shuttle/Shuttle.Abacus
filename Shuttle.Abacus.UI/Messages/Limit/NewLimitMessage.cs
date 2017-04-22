using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.Limit
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
