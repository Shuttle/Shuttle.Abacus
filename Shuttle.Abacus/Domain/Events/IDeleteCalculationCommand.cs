using System;

namespace Shuttle.Abacus
{
    public interface IDeleteCalculationCommand
    {
        Guid CalculationId { get; set; }
        Guid MethodId { get; set; }
    }
}
