using System;
using System.Data;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public static class FormulaColumns
    {
        public static readonly MappedColumn<Guid> Id = new MappedColumn<Guid>("FormulaId", DbType.Guid);
        public static readonly MappedColumn<string> Name = new MappedColumn<string>("Name", DbType.AnsiString, 120);
        public static readonly MappedColumn<string> MaximumFormulaName = new MappedColumn<string>("MaximumFormulaName", DbType.AnsiString, 120);
        public static readonly MappedColumn<string> MinimumFormulaName = new MappedColumn<string>("MinimumFormulaName", DbType.AnsiString, 120);

        public static class ConstraintColumns
        {
            public static readonly MappedColumn<int> SequenceNumber = new MappedColumn<int>("SequenceNumber", DbType.Int32);
            public static readonly MappedColumn<string> ArgumentName = new MappedColumn<string>("ArgumentName", DbType.AnsiString,
                120);
            public static readonly MappedColumn<string> ComparisonType = new MappedColumn<string>("ComparisonType", DbType.AnsiString, 120);
            public static readonly MappedColumn<string> Value = new MappedColumn<string>("Value", DbType.AnsiString, 120);
        }
    }
}