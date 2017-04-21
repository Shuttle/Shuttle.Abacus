using System;

namespace Abacus.CommandMediators
{
    public interface IDeleteCalculationCommand
    {
        Guid CalculationId { get; set; }
        Guid MethodId { get; set; }
    }
}
