using System;
using System.Data;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public static class FormulaColumns
    {
        public static readonly MappedColumn<Guid> FormulaId = new MappedColumn<Guid>("FormulaId", DbType.Guid);
        public static readonly MappedColumn<string> Name = new MappedColumn<string>("Name", DbType.AnsiString);

        public static readonly MappedColumn<string> MaximumFormulaName =
            new MappedColumn<string>("MaximumFormulaName", DbType.AnsiString);

        public static readonly MappedColumn<string> MinimumFormulaName =
            new MappedColumn<string>("MinimumFormulaName", DbType.AnsiString);

        public static readonly MappedColumn<string> ExecutionType =
            new MappedColumn<string>("ExecutionType", DbType.AnsiString);

        public static class OperationColumns
        {
            public static readonly MappedColumn<string> Operation =
                new MappedColumn<string>("Operation", DbType.AnsiString);

            public static readonly MappedColumn<int> SequenceNumber =
                new MappedColumn<int>("SequenceNumber", DbType.Int32);

            public static readonly MappedColumn<string> ValueSelection =
                new MappedColumn<string>("ValueSelection", DbType.AnsiString);

            public static readonly MappedColumn<string> ValueSource =
                new MappedColumn<string>("ValueSource", DbType.AnsiString);
        }

        public static class ConstraintColumns
        {
            public static readonly MappedColumn<int> SequenceNumber =
                new MappedColumn<int>("SequenceNumber", DbType.Int32);

            public static readonly MappedColumn<string> ArgumentName =
                new MappedColumn<string>("ArgumentName", DbType.AnsiString);

            public static readonly MappedColumn<string> Comparison =
                new MappedColumn<string>("Comparison", DbType.AnsiString);

            public static readonly MappedColumn<string> Value = new MappedColumn<string>("Value", DbType.AnsiString);
        }
    }
}