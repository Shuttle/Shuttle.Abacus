using System;

namespace Shuttle.Abacus.Events.Test.v1
{
    public class Registered
    {
        public string Name { get; set; }
        public string ExpectedResult { get; set; }
        public string ExpectedResultType { get; set; }
        public string Comparison { get; set; }
        public Guid FormulaId { get; set; }
    }
}