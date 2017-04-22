using System;
using System.Data;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class DecimalValueColumns
    {
        public static readonly MappedColumn<Guid> Id = new MappedColumn<Guid>("DecimalValueId", DbType.Guid);
        public static readonly MappedColumn<Guid> DecimalTableId = new MappedColumn<Guid>("DecimalTableId", DbType.Guid);
        public static readonly MappedColumn<int> ColumnIndex = new MappedColumn<int>("ColumnIndex", DbType.Int32);
        public static readonly MappedColumn<int> RowIndex = new MappedColumn<int>("RowIndex", DbType.Int32);
        public static readonly MappedColumn<decimal> DecimalValue = new MappedColumn<decimal>("DecimalValue", DbType.Decimal);
    }
}
