using System;
using System.Data;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class MatrixColumns
    {
        public static readonly MappedColumn<Guid> Id = new MappedColumn<Guid>("MatrixId", DbType.Guid);
        public static readonly MappedColumn<string> Name = new MappedColumn<string>("Name", DbType.AnsiString);
        public static readonly MappedColumn<string> RowArgumentName = new MappedColumn<string>("RowArgumentName", DbType.AnsiString);
        public static readonly MappedColumn<string> ColumnArgumentName = new MappedColumn<string>("ColumnArgumentName", DbType.AnsiString);
        public static readonly MappedColumn<string> ValueType = new MappedColumn<string>("ValueType", DbType.AnsiString);

        public class ElementColumns
        {
            public static readonly MappedColumn<int> Column = new MappedColumn<int>("Column", DbType.Int32);
            public static readonly MappedColumn<int> Row = new MappedColumn<int>("Row", DbType.Int32);
            public static readonly MappedColumn<decimal> Value = new MappedColumn<decimal>("Value", DbType.AnsiString);
        }

        public static class ConstraintColumns
        {
            public static readonly MappedColumn<string> Axis = new MappedColumn<string>("Axis", DbType.AnsiString);
            public static readonly MappedColumn<int> SequenceNumber = new MappedColumn<int>("SequenceNumber", DbType.Int32);
            public static readonly MappedColumn<string> Comparison = new MappedColumn<string>("Comparison", DbType.AnsiString);
            public static readonly MappedColumn<string> Value = new MappedColumn<string>("Value", DbType.AnsiString);
        }
    }
}
