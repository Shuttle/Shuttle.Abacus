using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.Resources;

namespace Shuttle.Abacus.Shell.Messages.Section
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

        public override IPermission RequiredPermission => Permissions.Formula;

        public string Text { get; private set; }

        public Resource OwnerResource { get; private set; }
    }
}
