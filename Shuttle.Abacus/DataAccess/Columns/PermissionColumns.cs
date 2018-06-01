using System;
using System.Data;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public static class PermissionColumns
    {
        public static readonly MappedColumn<string> Permission =
            new MappedColumn<string>("Permission", DbType.AnsiString);

        public static readonly MappedColumn<Guid> SystemUserId =
            new MappedColumn<Guid>("SystemUserID", DbType.Guid);
    }
}