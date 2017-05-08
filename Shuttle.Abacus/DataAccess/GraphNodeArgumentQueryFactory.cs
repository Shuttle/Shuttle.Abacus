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

        public static IQuery Add(Formula formula, GraphNodeArgument item, int sequence)
        {
            return RawQuery.Create(@"
insert into GraphNodeArgument
(
    CalculationId,
    ArgumentName,
    Format,
    SequenceNumber
)
values
(
    @CalculationId,
    @ArgumentName,
    @Format,
    @SequenceNumber
)
")
                .AddParameterValue(GraphNodeArgumentColumns.CalculationId, formula.Id)
                .AddParameterValue(GraphNodeArgumentColumns.SequenceNumber, sequence)
                .AddParameterValue(GraphNodeArgumentColumns.ArgumentId, item.Argument.Id)
                .AddParameterValue(GraphNodeArgumentColumns.Format, item.Format);
        }
        
        public static IQuery AllForFormula(Formula formula)
        {
            return RawQuery.Create(@"
select
    ArgumentName,
    Format
from
    GraphNodeArgument
where
    CalculationId = @CalculationId
order by
    SequenceNumber
")
                .AddParameterValue(GraphNodeArgumentColumns.CalculationId, formula.Id);
        }
    }
}
