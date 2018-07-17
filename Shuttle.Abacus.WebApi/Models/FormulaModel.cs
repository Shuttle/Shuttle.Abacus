using System;

namespace Shuttle.Abacus.WebApi
{
    public class FormulaModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MaximumFormulaName { get; set; }
        public string MinimumFormulaName { get; set; }
    }
}