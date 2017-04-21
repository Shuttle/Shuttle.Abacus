using System;
using System.Collections.Generic;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.Domain
{
    public class SetCalculationConstraintsCommand 
    {
        public Guid CalculationId { get; set; }
        public List<ConstraintDTO> Constraints { get; set; }
    }
}
