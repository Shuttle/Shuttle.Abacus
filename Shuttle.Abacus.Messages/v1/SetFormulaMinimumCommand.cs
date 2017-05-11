using System;

namespace Shuttle.Abacus.Messages.v1
{
    public class SetFormulaMinimumCommand
    {
        public Guid FormulaId { get; set; }
        public string MinimumFormulaName { get; set; }
    }
}