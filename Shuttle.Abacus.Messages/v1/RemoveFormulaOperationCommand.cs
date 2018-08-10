using System;

namespace Shuttle.Abacus.Messages.v1
{
    public class RemoveFormulaOperationCommand
    {
        public Guid FormulaId { get; set; }
        public Guid OperationId { get; set; }
    }
}
