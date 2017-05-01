using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.Domain
{
    public class CreateFormulaCommand 
    {
        public string OwnerName { get; set; }
        public Guid OwnerId { get; set; }
        public List<FormulaOperation> Operations { get; set; }
        public List<OwnedConstraint> Constraints { get; set; }
    }
}
