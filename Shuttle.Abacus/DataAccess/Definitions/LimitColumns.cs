using System;
using System.Data;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public static class LimitColumns
    {
        public static readonly MappedColumn<Guid> OwnerId = new MappedColumn<Guid>("OwnerId", DbType.Guid);

        public static readonly MappedColumn<string> OwnerName = new MappedColumn<string>("OwnerName",
                                                                                               DbType.AnsiString, 100);

        public static readonly MappedColumn<Guid> Id =
            new MappedColumn<Guid>("LimitId", DbType.Guid);

        public static readonly MappedColumn<string> Name = new MappedColumn<string>("Name", DbType.AnsiString, 100);
        public static readonly MappedColumn<string> Type = new MappedColumn<string>("Type", DbType.AnsiString, 20);
    }
}
