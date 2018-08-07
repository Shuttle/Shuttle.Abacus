using System;

namespace Shuttle.Abacus.WebApi
{
    public class FormulaConstraintModel
    {
        public Guid Id { get; set; }
        public Guid FormulaId { get; set; }
        public Guid ArgumentId { get; set; }
        public string ArgumentName { get; set; }
        public string Comparison { get; set; }
        public string Value { get; set; }
    }
}