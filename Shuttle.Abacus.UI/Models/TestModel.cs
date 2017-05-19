using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.Models
{
    public class TestModel
    {
        public TestModel()
        {
        }

        public TestModel(DataRow row)
        {
            Guard.AgainstNull(row, "row");

            FormulaName = TestColumns.FormulaName.MapFrom(row);
            Name = TestColumns.Name.MapFrom(row);
            Comparison = TestColumns.Comparison.MapFrom(row);
            ExpectedResult = TestColumns.ExpectedResult.MapFrom(row);
            ExpectedResultType = TestColumns.ExpectedResultType.MapFrom(row);
        }

        public IEnumerable<string> Formulas { get; set; }

        public string Name { get; set; }
        public string Comparison { get; set; }
        public string ExpectedResultType { get; set; }
        public string ExpectedResult { get; set; }
        public string FormulaName { get; set; }
    }
}