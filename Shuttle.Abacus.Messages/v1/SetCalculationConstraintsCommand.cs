using System;
using System.Collections.Generic;
using Shuttle.Abacus.Messages.v1.TransferObjects;

namespace Shuttle.Abacus.Messages.v1
{
    public class SetCalculationConstraintsCommand 
    {
        public Guid CalculationId { get; set; }
        public List<Constraint> Constraints { get; set; }
    }
}
