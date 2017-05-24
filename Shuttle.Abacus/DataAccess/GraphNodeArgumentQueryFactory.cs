using System;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class GraphNodeArgumentQueryFactory
    {
        public static IQuery RemoveFor(Guid calculationId)
        {
            return RawQuery.Create(@"
delete
from
    GraphNodeArgument
where
    CalculationId = @CalculationId")
                .AddParameterValue(GraphNodeArgumentColumns.CalculationId, calculationId);
        }
    }
}
