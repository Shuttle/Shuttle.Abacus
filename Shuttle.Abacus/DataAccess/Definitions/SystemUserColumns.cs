using System;
using System.Data;

namespace Abacus.Data
{
    public static class SystemUserColumns
    {
        public static readonly QueryColumn<Guid> Id =
            new QueryColumn<Guid>("SystemUserID", DbType.Guid).AsIdentifier();

        public static readonly QueryColumn<Guid> IdAliased =
            new QueryColumn<Guid>("u.SystemUserID", DbType.Guid).AsIdentifier();

        public static readonly QueryColumn<string> LoginName = new QueryColumn<string>("LoginName",
                                                                                       DbType.AnsiString, 100);
    }
}
