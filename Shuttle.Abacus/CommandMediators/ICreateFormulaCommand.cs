using System;
using System.Collections.Generic;
using Abacus.DTO;

namespace Abacus.CommandMediators
{
    public interface ICreateFormulaCommand
    {
        string OwnerName { get; set; }
        Guid OwnerId { get; set; }
        List<OperationDTO> Operations { get; set; }
        List<ConstraintDTO> Constraints { get; set; }
    }
}
