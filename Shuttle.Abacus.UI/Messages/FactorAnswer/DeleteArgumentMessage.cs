using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.Resources;

namespace Shuttle.Abacus.UI.Messages.FactorAnswer
{
    public class DeleteArgumentMessage : Message
    {
        public DeleteArgumentMessage(Guid argumentId, string text, Resource ownerResource)
        {
            ArgumentId = argumentId;
            Text = text;
            OwnerResource = ownerResource;
        }

        public Guid ArgumentId { get; private set; }
        public string Text { get; private set; }
        public Resource OwnerResource { get; set; }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Argument; }
        }
    }
}
