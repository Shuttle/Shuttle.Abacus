using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.Formula
{
    public class ChangeFormulaOrderMessage : Message
    {
        public ChangeFormulaOrderMessage(Guid methodId, string ownerName, Guid ownerId,
                                         string ownerText)
        {
            MethodId = methodId;
            OwnerName = ownerName;
            OwnerId = ownerId;
            OwnerText = ownerText;
        }

        public Guid MethodId { get; private set; }
        public string OwnerName { get; private set; }
        public Guid OwnerId { get; private set; }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Method; }
        }

        public string OwnerText { get; private set; }
    }
}
