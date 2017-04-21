using System;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class DeleteLimitMessage : Message
    {
        public Guid LimitId { get; private set; }
        public string Text { get; private set; }
        public Resource OwnerResource { get; private set; }

        public DeleteLimitMessage(Guid limitId, string text, Resource ownerResource)
        {
            LimitId = limitId;
            Text = text;
            OwnerResource = ownerResource;
        }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Method; }
        }
    }
}
