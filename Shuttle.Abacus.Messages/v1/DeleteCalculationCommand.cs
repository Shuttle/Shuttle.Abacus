using System;

namespace Shuttle.Abacus.Messages.v1
{
    public class DeleteCalculationCommand 
    {
        public Guid CalculationId { get; set; }
        public Guid MethodId { get; set; }
    }
}
