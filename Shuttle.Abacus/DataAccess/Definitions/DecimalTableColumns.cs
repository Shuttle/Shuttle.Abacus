using System;
using System.Data;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess.Definitions
{
    public class DecimalTableColumns
    {
        public static readonly MappedColumn<Guid> Id = new MappedColumn<Guid>("DecimalTableId", DbType.Guid);
        public static readonly MappedColumn<string> Name = new MappedColumn<string>("Name", DbType.AnsiString, 160);
        public static readonly MappedColumn<Guid> RowArgumentId = new MappedColumn<Guid>("RowArgumentId", DbType.Guid);
        public static readonly MappedColumn<Guid> ColumnArgumentId = new MappedColumn<Guid>("ColumnArgumentId", DbType.Guid);
    }
}
