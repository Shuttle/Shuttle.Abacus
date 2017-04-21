using System;
using System.Data;

namespace Abacus.Data
{
    public static class PermissionColumns
    {
        public static readonly QueryColumn<string> Permission =
            new QueryColumn<string>("Permission", DbType.AnsiString, 100);

        public static readonly QueryColumn<Guid> SystemUserId =
            new QueryColumn<Guid>("SystemUserID", DbType.Guid);
    }
}
