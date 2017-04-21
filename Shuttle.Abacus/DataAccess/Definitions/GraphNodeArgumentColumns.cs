using System;
using System.Data;

namespace Abacus.Data
{
    public class GraphNodeArgumentColumns
    {
        public static readonly QueryColumn<Guid> CalculationId = new QueryColumn<Guid>("CalculationId", DbType.Guid).AsIdentifier();
        public static readonly QueryColumn<int> SequenceNumber = new QueryColumn<int>("SequenceNumber", DbType.Int32);
        public static readonly QueryColumn<Guid> ArgumentId = new QueryColumn<Guid>("ArgumentId", DbType.Guid).AsIdentifier();
        public static readonly QueryColumn<string> Format = new QueryColumn<string>("Format", DbType.AnsiString, 250);
    }
}
