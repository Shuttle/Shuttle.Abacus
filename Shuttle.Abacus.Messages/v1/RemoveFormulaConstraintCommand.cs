using System;

namespace Shuttle.Abacus.Messages.v1
{
    public class RemoveFormulaConstraintCommand
    {
        public Guid FormulaId { get; set; }
        public Guid ConstraintId { get; set; }
    }
}
