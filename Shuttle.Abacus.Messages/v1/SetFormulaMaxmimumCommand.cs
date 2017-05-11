using System;

namespace Shuttle.Abacus.Messages.v1
{
    public class SetFormulaMaxmimumCommand
    {
        public Guid FormulaId { get; set; }
        public string MaximumFormulaName { get; set; }
    }
}