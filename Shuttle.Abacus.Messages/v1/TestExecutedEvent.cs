using System;

namespace Shuttle.Abacus.Messages.v1
{
    public class TestExecutedEvent
    {
        public Guid Id { get; set; }
        public string FormulaName { get; set; }
        public decimal Result { get; set; }
    }
}