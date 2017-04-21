using System;

namespace Shuttle.Abacus
{
    public class DeleteCalculationCommand : IDeleteCalculationCommand
    {
        public Guid CalculationId { get; set; }
        public Guid MethodId { get; set; }
    }
}
