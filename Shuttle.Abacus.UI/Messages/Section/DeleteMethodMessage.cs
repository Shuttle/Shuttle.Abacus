using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.Resources;

namespace Shuttle.Abacus.UI.Messages.Section
{
    public class DeleteMethodMessage : Message
    {
        public Guid MethodId { get; private set; }

        public DeleteMethodMessage(Guid methodId, string text, Resource ownerResource)
        {
            MethodId = methodId;
            Text = text;
            OwnerResource = ownerResource;
        }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Method; }
        }

        public string Text { get; private set; }

        public Resource OwnerResource { get; private set; }
    }
}
