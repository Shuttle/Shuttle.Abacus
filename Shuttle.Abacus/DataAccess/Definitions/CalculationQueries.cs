using System;

namespace Abacus.Data
{
    public class CalculationQueries
    {
        private const string TableName = "Calculation";
        private const string GraphNodeArgumentTableName = "GraphNodeArgument";

        public static ISelectQuery AllForOwner(Guid ownerId)
        {
            return SelectBuilder
                .Select(CalculationColumns.Id)
                .With(CalculationColumns.Type)
                .With(CalculationColumns.Name)
                .With(CalculationColumns.OwnerName)
                .Where(CalculationColumns.OwnerId).EqualTo(ownerId)
                .OrderBy(CalculationColumns.SequenceNumber).Ascending()
                .From(TableName);
        }

        public static ISelectQuery Get(Guid id)
        {
            return SelectBuilder
                .Select(CalculationColumns.Id)
                .With(CalculationColumns.Type)
                .With(CalculationColumns.Name)
                .With(CalculationColumns.OwnerName)
                .With(CalculationColumns.Required)
                .Where(CalculationColumns.Id).EqualTo(id)
                .From(TableName);
        }

        public static ISelectQuery AllForMethod(Guid methodId)
        {
            return SelectBuilder
                .Select(CalculationColumns.Id)
                .With(CalculationColumns.Name)
                .Where(CalculationColumns.MethodId).EqualTo(methodId)
                .OrderBy(CalculationColumns.SequenceNumber).Ascending()
                .From(TableName);
        }

        public static ISelectQuery AllBeforeCalculation(Guid methodId, Guid calculationId)
        {
            var query = SelectQuery.CreateSelectFrom(
                DynamicQuery.CreateFrom(
                    @"
                select 
                    CalculationId,
                    Name
                from
                    Calculation
                where
                    MethodId = @MethodId
                and
                    SequenceNumber <
                             (select SequenceNumber
                              from Calculation AS Calculation_SequenceNumber
                              where (CalculationId = @CalculationId)) ")
                    .Build());

            query.AddParameterValue(CalculationColumns.MethodId, methodId);
            query.AddParameterValue(CalculationColumns.Id, calculationId);

            query.AddColumn(CalculationColumns.Id);
            query.AddColumn(CalculationColumns.Name);

            return query;
        }

        public static ISelectQuery Name(Guid id)
        {
            return SelectBuilder
                .Select(CalculationColumns.Name)
                .Where(CalculationColumns.Id).EqualTo(id)
                .From(TableName);
        }

        public static ISelectQuery AllForMethod(Guid methodId, Guid grabberCalculationId)
        {
            return SelectBuilder
                .Select(CalculationColumns.Id)
                .With(CalculationColumns.Name)
                .Where(CalculationColumns.MethodId).EqualTo(methodId)
                .And(CalculationColumns.Id).NotEqualTo(grabberCalculationId)
                .OrderBy(CalculationColumns.SequenceNumber).Ascending()
                .From(TableName);
        }

        public static ISelectQuery GraphNodeArguments(Guid calculationId)
        {
            return SelectBuilder
                .Select(GraphNodeArgumentColumns.ArgumentId)
                .With(GraphNodeArgumentColumns.Format)
                .Where(GraphNodeArgumentColumns.CalculationId).EqualTo(calculationId)
                .OrderBy(GraphNodeArgumentColumns.SequenceNumber).Ascending()
                .From(GraphNodeArgumentTableName);
        }
    }
}
