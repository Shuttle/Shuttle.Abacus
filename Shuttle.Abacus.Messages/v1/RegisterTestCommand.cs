using System;

namespace Shuttle.Abacus.Messages.v1
{
    public class RegisterTestCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid FormulaId { get; set; }
        public string ExpectedResult { get; set; }
        public string ExpectedResultDataTypeName { get; set; }
        public string Comparison { get; set; }
    }
}