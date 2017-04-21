using System;
using System.Data;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess.Definitions
{
    public class GraphNodeArgumentColumns
    {
        public static readonly MappedColumn<Guid> CalculationId = new MappedColumn<Guid>("CalculationId", DbType.Guid);
        public static readonly MappedColumn<int> SequenceNumber = new MappedColumn<int>("SequenceNumber", DbType.Int32);
        public static readonly MappedColumn<Guid> ArgumentId = new MappedColumn<Guid>("ArgumentId", DbType.Guid);
        public static readonly MappedColumn<string> Format = new MappedColumn<string>("Format", DbType.AnsiString, 250);
    }
}
