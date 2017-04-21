using System;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class ManageCalculationConstraintsMessage : Message
    {
        public string CalculationName { get; private set; }
        public Guid CalculationId { get; private set; }

        public ManageCalculationConstraintsMessage(string calculationName, Guid calculationId)
        {
            CalculationName = calculationName;
            CalculationId = calculationId;
        }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Method; }
        }
    }
}
