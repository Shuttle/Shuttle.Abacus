using System;

namespace Shuttle.Abacus.Domain
{
    public class DeleteCalculationCommand 
    {
        public Guid CalculationId { get; set; }
        public Guid MethodId { get; set; }
    }
}
