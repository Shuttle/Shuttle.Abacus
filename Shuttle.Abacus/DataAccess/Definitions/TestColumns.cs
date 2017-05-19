using System;
using System.Data;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public static class TestColumns
    {
        public static readonly MappedColumn<string> Name =
            new MappedColumn<string>("Name", DbType.AnsiString);

        public static readonly MappedColumn<string> ExpectedResult =
            new MappedColumn<string>("ExpectedResult", DbType.AnsiString);

        public static readonly MappedColumn<string> ExpectedResultType =
            new MappedColumn<string>("ExpectedResultType", DbType.AnsiString);

        public static readonly MappedColumn<string> Comparison =
            new MappedColumn<string>("Comparison", DbType.AnsiString);

        public static readonly MappedColumn<Guid> Id =
            new MappedColumn<Guid>("TestId", DbType.Guid);

        public static readonly MappedColumn<string> FormulaName =
            new MappedColumn<string>("FormulaName", DbType.AnsiString);

        public static class ArgumentValueColumns
        {
            public static readonly MappedColumn<string>
                Value = new MappedColumn<string>("Value", DbType.AnsiString);

            public static readonly MappedColumn<string> ArgumentName = new MappedColumn<string>("ArgumentName", DbType.AnsiString);
        }
    }
}
