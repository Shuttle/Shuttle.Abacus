using System;
using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface ICreateFormulaCommand
    {
        string OwnerName { get; set; }
        Guid OwnerId { get; set; }
        List<OperationDTO> Operations { get; set; }
        List<ConstraintDTO> Constraints { get; set; }
    }
}
