using System;
using System.Collections.Generic;
using Shuttle.Abacus.Messages.v1.TransferObjects;

namespace Shuttle.Abacus.Messages.v1
{
    public class SetFormulaConstraintsCommand
    {
        public Guid FormulaId { get; set; }
        public List<FormulaConstraint> Constraints { get; set; }
    }
}