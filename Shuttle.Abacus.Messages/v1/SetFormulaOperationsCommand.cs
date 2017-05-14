using System;
using System.Collections.Generic;
using Shuttle.Abacus.Messages.v1.TransferObjects;

namespace Shuttle.Abacus.Messages.v1
{
    public class SetFormulaOperationsCommand
    {
        public Guid FormulaId { get; set; }
        public List<FormulaOperation> Operations { get; set; }
    }
}