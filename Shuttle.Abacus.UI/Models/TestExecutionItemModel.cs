using System;
using System.Data;
using Shuttle.Abacus.DataAccess;

namespace Shuttle.Abacus.Shell.Models
{
    public class TestExecutionItemModel
    {
        public TestExecutionItemModel(DataRow row)
        {
            Id = TestColumns.Id.MapFrom(row);
            Name = TestColumns.Name.MapFrom(row);
            ExpectedResult = TestColumns.ExpectedResult.MapFrom(row);
            ExpectedResultType = TestColumns.ExpectedResultType.MapFrom(row);
            Comparison = TestColumns.Comparison.MapFrom(row);
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string ExpectedResult { get; private set; }
        public string ExpectedResultType { get; private set; }
        public string Comparison { get; private set; }
    }
}