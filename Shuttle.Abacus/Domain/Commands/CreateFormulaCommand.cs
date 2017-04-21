using System;
using System.Collections.Generic;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.Domain
{
    public class CreateFormulaCommand 
    {
        public string OwnerName { get; set; }
        public Guid OwnerId { get; set; }
        public List<OperationDTO> Operations { get; set; }
        public List<ConstraintDTO> Constraints { get; set; }
    }
}
