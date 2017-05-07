using System;
using System.Collections.Generic;
using Shuttle.Abacus.Messages.v1.TransferObjects;

namespace Shuttle.Abacus.Messages.v1
{
    public class ChangeFormulaCommand
    {
        public Guid FormulaId { get; set; }
        public string Name { get; set; }
        public List<FormulaOperation> Operations { get; set; }
        public List<Constraint> Constraints { get; set; }
    }
}