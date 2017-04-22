using System;
using Shuttle.Abacus.Domain;
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

        public static IQuery Add(Calculation calculation, GraphNodeArgument item, int sequence)
        {
            return RawQuery.Create(@"
insert into GraphNodeArgument
(
    CalculationId,
    ArgumentId,
    Format,
    SequenceNumber
)
values
(
    @CalculationId,
    @ArgumentId,
    @Format,
    @SequenceNumber
)
")
                .AddParameterValue(GraphNodeArgumentColumns.CalculationId, calculation.Id)
                .AddParameterValue(GraphNodeArgumentColumns.SequenceNumber, sequence)
                .AddParameterValue(GraphNodeArgumentColumns.ArgumentId, item.Argument.Id)
                .AddParameterValue(GraphNodeArgumentColumns.Format, item.Format);
        }
        
        public static IQuery AllForCalculation(Calculation calculation)
        {
            return RawQuery.Create(@"
select
    ArgumentId,
    Format
from
    GraphNodeArgument
where
    CalculationId = @CalculationId
order by
    SequenceNumber
")
                .AddParameterValue(GraphNodeArgumentColumns.CalculationId, calculation.Id);
        }
    }
}
