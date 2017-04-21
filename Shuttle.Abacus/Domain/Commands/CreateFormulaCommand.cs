using System;
using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public class CreateFormulaCommand : ICreateFormulaCommand
    {
        public string OwnerName { get; set; }
        public Guid OwnerId { get; set; }
        public List<OperationDTO> Operations { get; set; }
        public List<ConstraintDTO> Constraints { get; set; }
    }
}
