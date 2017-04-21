using System;
using Abacus.Domain;

namespace Abacus.Data
{
    public class GraphNodeArgumentTableAccess
    {
        public const string TableName = "GraphNodeArgument";

        public static IQuery RemoveFor(Guid calculationId)
        {
            return DeleteBuilder
                .Where(GraphNodeArgumentColumns.CalculationId).EqualTo(calculationId)
                .From(TableName);
        }

        public static IQuery Add(Calculation calculation, GraphNodeArgument item, int sequence)
        {
            return InsertBuilder
                .Insert()
                .Add(GraphNodeArgumentColumns.CalculationId).WithValue(calculation.Id)
                .Add(GraphNodeArgumentColumns.SequenceNumber).WithValue(sequence)
                .Add(GraphNodeArgumentColumns.ArgumentId).WithValue(item.Argument.Id)
                .Add(GraphNodeArgumentColumns.Format).WithValue(item.Format)
                .Into(TableName);
        }


        public static IQuery AllForCalculation(Calculation calculation)
        {
            return SelectBuilder
                .Select(GraphNodeArgumentColumns.ArgumentId)
                .With(GraphNodeArgumentColumns.Format)
                .Where(GraphNodeArgumentColumns.CalculationId).EqualTo(calculation.Id)
                .OrderBy(GraphNodeArgumentColumns.SequenceNumber).Ascending()
                .From(TableName);
        }
    }
}
