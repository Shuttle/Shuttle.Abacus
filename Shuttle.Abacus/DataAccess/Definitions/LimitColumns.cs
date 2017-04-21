using System;
using System.Data;

namespace Abacus.Data
{
    public static class LimitColumns
    {
        public static readonly QueryColumn<Guid> OwnerId = new QueryColumn<Guid>("OwnerId", DbType.Guid);

        public static readonly QueryColumn<string> OwnerName = new QueryColumn<string>("OwnerName",
                                                                                               DbType.AnsiString, 100);

        public static readonly QueryColumn<Guid> Id =
            new QueryColumn<Guid>("LimitId", DbType.Guid).AsIdentifier();

        public static readonly QueryColumn<string> Name = new QueryColumn<string>("Name", DbType.AnsiString, 100);
        public static readonly QueryColumn<string> Type = new QueryColumn<string>("Type", DbType.AnsiString, 20);
    }
}
