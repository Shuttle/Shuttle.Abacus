using System;

namespace Shuttle.Abacus.DataAccess.Definitions
{
    public class FormulaQueries
    {
        public const string OperationTableName = "FormulaOperation";
        

        public IQuery AllForOwner(Guid ownerId)
        {
            return RawQuery.Create(@"
select
                Id,
                Description,
                .AddParameterValue(FormulaColumns.OwnerId, ownerId)
                .From(TableName);
        }


        public IQuery GetOperations(Guid id)
        {
            return RawQuery.Create(@"
select
                Operation,
                ValueSource,
                ValueSelection,
                Text,
                .AddParameterValue(FormulaOperationColumns.FormulaId, id)
                .From(OperationTableName);
        }

        public IQuery Description(Guid id)
        {
            return RawQuery.Create(@"
select
                Description,
                .AddParameterValue(FormulaColumns.Id, id)
                .From(TableName);
        }

        public IQuery Get(Guid id)
        {
            return RawQuery.Create(@"
select
                OwnerName,
                OwnerId,
                .AddParameterValue(FormulaColumns.Id, id)
                .From(TableName);
        }

        public IQuery OperationsSummary(Guid id)
        {
            return RawQuery.Create(@"
select
                Operation,
                ValueSource,
                Text,
                .AddParameterValue(FormulaOperationColumns.FormulaId, id)
                .From(OperationTableName);
        }
    }
}
