using System;
using System.Data;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public static class FormulaColumns
    {
        public static readonly MappedColumn<Guid> OwnerId = new MappedColumn<Guid>("OwnerId", DbType.Guid);

        public static readonly MappedColumn<string> OwnerName = new MappedColumn<string>("OwnerName",
                                                                                               DbType.AnsiString, 100);

        public static readonly MappedColumn<string> Description = new MappedColumn<string>("Description",
                                                                                         DbType.AnsiString, 2000);

        public static readonly MappedColumn<Guid> Id = new MappedColumn<Guid>("FormulaId", DbType.Guid);
        public static readonly MappedColumn<int> SequenceNumber = new MappedColumn<int>("SequenceNumber", DbType.Int32);
    }
}
