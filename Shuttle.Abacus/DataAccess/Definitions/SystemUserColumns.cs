using System;
using System.Data;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess.Definitions
{
    public static class SystemUserColumns
    {
        public static readonly MappedColumn<Guid> Id =
            new MappedColumn<Guid>("SystemUserID", DbType.Guid);

        public static readonly MappedColumn<Guid> IdAliased =
            new MappedColumn<Guid>("u.SystemUserID", DbType.Guid);

        public static readonly MappedColumn<string> LoginName = new MappedColumn<string>("LoginName",
                                                                                       DbType.AnsiString, 100);
    }
}
