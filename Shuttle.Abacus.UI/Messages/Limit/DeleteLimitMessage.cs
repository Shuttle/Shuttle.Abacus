using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.Resources;

namespace Shuttle.Abacus.UI.Messages.Limit
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
