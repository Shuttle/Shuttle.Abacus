using System;
using System.Data;

namespace Abacus.Data
{
    public static class FormulaColumns
    {
        public static readonly QueryColumn<Guid> OwnerId = new QueryColumn<Guid>("OwnerId", DbType.Guid);

        public static readonly QueryColumn<string> OwnerName = new QueryColumn<string>("OwnerName",
                                                                                               DbType.AnsiString, 100);

        public static readonly QueryColumn<string> Description = new QueryColumn<string>("Description",
                                                                                         DbType.AnsiString, 2000);

        public static readonly QueryColumn<Guid> Id = new QueryColumn<Guid>("FormulaId", DbType.Guid).AsIdentifier();
        public static readonly QueryColumn<int> SequenceNumber = new QueryColumn<int>("SequenceNumber", DbType.Int32);
    }
}
