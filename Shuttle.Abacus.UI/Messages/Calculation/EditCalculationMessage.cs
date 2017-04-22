using System;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.Calculation
{
    public class EditCalculationMessage : Message
    {
        public EditCalculationMessage()
        {
        }

        public EditCalculationMessage(Guid calculationId, string ownerName, Guid ownerId, Guid methodId)
        {
            CalculationId = calculationId;
            MethodId = methodId;
            OwnerName = ownerName;
            OwnerId = ownerId;
        }

        public Guid CalculationId { get; private set; }
        public Guid OwnerId { get; private set; }
        public Guid MethodId { get; private set; }
        public string OwnerName { get; private set; }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Method; }
        }
    }
}
