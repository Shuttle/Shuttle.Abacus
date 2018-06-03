using System;

namespace Shuttle.Abacus.DataAccess.Query
{
    public class Formula
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MaximumFormulaName { get; set; }
        public string MinimumFormulaName { get; set; }
    }
}