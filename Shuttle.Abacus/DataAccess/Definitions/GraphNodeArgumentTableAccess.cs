using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess.Definitions
{
    public class GraphNodeArgumentTableAccess
    {
        

        public static IQuery RemoveFor(Guid calculationId)
        {
            return DeleteBuilder
                .AddParameterValue(GraphNodeArgumentColumns.CalculationId, calculationId)
                .From(TableName);
        }

        public static IQuery Add(Calculation calculation, GraphNodeArgument item, int sequence)
        {
            return InsertBuilder
                .Insert()
                .Add(GraphNodeArgumentColumns.CalculationId).WithValue(calculation.Id)
                .Add(GraphNodeArgumentColumns.SequenceNumber).WithValue(sequence)
                .Add(GraphNodeArgumentColumns.ArgumentId).WithValue(item.Argument.Id)
                .AddParameterValue(GraphNodeArgumentColumns.Format, item.Format)
                .Into(TableName);
        }


        public static IQuery AllForCalculation(Calculation calculation)
        {
            return RawQuery.Create(@"
select
                ArgumentId,
                Format,
                .AddParameterValue(GraphNodeArgumentColumns.CalculationId, calculation.Id)
                .OrderBy(GraphNodeArgumentColumns.SequenceNumber).Ascending()
                .From(TableName);
        }
    }
}
