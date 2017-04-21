using System;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class DeleteCalculationMessage : Message
    {
        public DeleteCalculationMessage(Resource ownerResource, string text, Guid calculationId, Guid methodId)
        {
            OwnerResource = ownerResource;
            Text = text;
            CalculationId = calculationId;
            MethodId = methodId;
        }

        public Resource OwnerResource { get; private set; }
        public string Text { get; private set; }
        public Guid CalculationId { get; private set; }
        public Guid MethodId { get; private set; }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Method; }
        }
    }
}
