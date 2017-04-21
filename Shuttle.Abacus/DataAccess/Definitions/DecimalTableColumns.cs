using System;
using System.Data;

namespace Abacus.Data
{
    public class DecimalTableColumns
    {
        public static readonly QueryColumn<Guid> Id = new QueryColumn<Guid>("DecimalTableId", DbType.Guid).AsIdentifier();
        public static readonly QueryColumn<string> Name = new QueryColumn<string>("Name", DbType.AnsiString, 160);
        public static readonly QueryColumn<Guid> RowArgumentId = new QueryColumn<Guid>("RowArgumentId", DbType.Guid);
        public static readonly QueryColumn<Guid> ColumnArgumentId = new QueryColumn<Guid>("ColumnArgumentId", DbType.Guid);
    }
}
