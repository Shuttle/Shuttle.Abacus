using System;
using System.Data;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class MatrixColumns
    {
        public static readonly MappedColumn<Guid> Id = new MappedColumn<Guid>("MatrixId", DbType.Guid);
        public static readonly MappedColumn<string> Name = new MappedColumn<string>("Name", DbType.AnsiString, 160);
        public static readonly MappedColumn<Guid> RowArgumentId = new MappedColumn<Guid>("RowArgumentId", DbType.Guid);
        public static readonly MappedColumn<Guid> ColumnArgumentId = new MappedColumn<Guid>("ColumnArgumentId", DbType.Guid);

        public class ElementColumns
        {
            public static readonly MappedColumn<Guid> Id = new MappedColumn<Guid>("DecimalValueId", DbType.Guid);
            public static readonly MappedColumn<Guid> MatrixId = new MappedColumn<Guid>("MatrixId", DbType.Guid);
            public static readonly MappedColumn<int> ColumnIndex = new MappedColumn<int>("ColumnIndex", DbType.Int32);
            public static readonly MappedColumn<int> RowIndex = new MappedColumn<int>("RowIndex", DbType.Int32);
            public static readonly MappedColumn<decimal> DecimalValue = new MappedColumn<decimal>("MatrixElement", DbType.Decimal);
        }
    }
}
