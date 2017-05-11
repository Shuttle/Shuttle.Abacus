using System;

namespace Shuttle.Abacus.Messages.v1
{
    public class RenameFormulaCommand
    {
        public Guid FormulaId { get; set; }
        public string Name { get; set; }
    }
}