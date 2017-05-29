using System;
using Shuttle.Abacus.Messages.v1.TransferObjects;

namespace Shuttle.Abacus.Messages.v1
{
    public class TestExecutedEvent
    {
        public Guid Id { get; set; }
        public string FormulaName { get; set; }
        public decimal Result { get; set; }
        public string Log { get; set; }
        public FormulaContext FormulaContext { get; set; }
        public string Exception { get; set; }
    }
}