using System;
using System.Data;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess.Definitions
{
    public static class CalculationColumns
    {
        public static readonly MappedColumn<Guid> OwnerId = new MappedColumn<Guid>("OwnerId", DbType.Guid);

        public static readonly MappedColumn<string> OwnerName = new MappedColumn<string>("OwnerName",
                                                                                               DbType.AnsiString, 100);

        public static readonly MappedColumn<Guid> Id = new MappedColumn<Guid>("CalculationId", DbType.Guid);

        public static readonly MappedColumn<string> Name = new MappedColumn<string>("Name", DbType.AnsiString, 100);
        public static readonly MappedColumn<bool> Required = new MappedColumn<bool>("Required", DbType.Byte);
        public static readonly MappedColumn<Guid> MethodId = new MappedColumn<Guid>("MethodId", DbType.Guid);

        public static readonly MappedColumn<int> SequenceNumber = new MappedColumn<int>("SequenceNumber", DbType.Int32);
        public static readonly MappedColumn<string> Type = new MappedColumn<string>("Type", DbType.AnsiString, 20);
    }
}
