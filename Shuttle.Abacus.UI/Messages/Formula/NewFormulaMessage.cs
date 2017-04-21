using System;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class NewFormulaMessage : Message
    {
        public NewFormulaMessage(Guid methodId, string ownerName, Guid ownerId)
        {
            MethodId = methodId;
            OwnerName = ownerName;
            OwnerId = ownerId;
        }

        public NewFormulaMessage(NewFormulaFromExistingMessage message)
            : this(message.MethodId, message.OwnerName, message.OwnerId)
        {
        }

        public Guid MethodId { get; private set; }
        public string OwnerName { get; private set; }
        public Guid OwnerId { get; private set; }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Method; }
        }
    }
}
