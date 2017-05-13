using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.Resources;

namespace Shuttle.Abacus.UI.Messages.Argument
{
    public class RemoveArgumentMessage : Message
    {
        public RemoveArgumentMessage(string text, Guid argumentId, Resource ownerResource)
        {
            ArgumentId = argumentId;
            Text = text;
            OwnerResource = ownerResource;
        }

        public Guid ArgumentId { get; private set; }
        public string Text { get; private set; }
        public Resource OwnerResource { get; set; }

        public override IPermission RequiredPermission => Permissions.Argument;
    }
}
