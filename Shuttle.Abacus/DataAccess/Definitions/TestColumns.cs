using System;
using System.Data;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public static class TestColumns
    {
        public static readonly MappedColumn<string> Description =
            new MappedColumn<string>("Description", DbType.AnsiString);

        public static readonly MappedColumn<string> ExpectedResult =
            new MappedColumn<string>("ExpectedResult", DbType.AnsiString);

        public static readonly MappedColumn<string> ExpectedResultType =
            new MappedColumn<string>("ExpectedResultType", DbType.AnsiString);

        public static readonly MappedColumn<string> ComparisonType =
            new MappedColumn<string>("ExpectedResultType", DbType.AnsiString);

        public static readonly MappedColumn<Guid> Id =
            new MappedColumn<Guid>("TestId", DbType.Guid);

        public static readonly MappedColumn<Guid> FormulaId =
            new MappedColumn<Guid>("FormulaId", DbType.Guid);

        public static class ArgumentValueColumns
        {
            public static readonly MappedColumn<string>
                Value = new MappedColumn<string>("Value", DbType.AnsiString);

            public static readonly MappedColumn<Guid> ArgumentName = new MappedColumn<Guid>("ArgumentName", DbType.Guid);

            public static readonly MappedColumn<Guid> TestId =
                new MappedColumn<Guid>("TestId", DbType.Guid);
        }
    }
}
