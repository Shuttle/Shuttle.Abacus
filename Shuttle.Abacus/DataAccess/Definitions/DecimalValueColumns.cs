using System;
using System.Data;

namespace Abacus.Data
{
    public class DecimalValueColumns
    {
        public static readonly QueryColumn<Guid> Id = new QueryColumn<Guid>("DecimalValueId", DbType.Guid).AsIdentifier();
        public static readonly QueryColumn<Guid> DecimalTableId = new QueryColumn<Guid>("DecimalTableId", DbType.Guid);
        public static readonly QueryColumn<int> ColumnIndex = new QueryColumn<int>("ColumnIndex", DbType.Int32);
        public static readonly QueryColumn<int> RowIndex = new QueryColumn<int>("RowIndex", DbType.Int32);
        public static readonly QueryColumn<decimal> DecimalValue = new QueryColumn<decimal>("DecimalValue", DbType.Decimal);
    }
}
