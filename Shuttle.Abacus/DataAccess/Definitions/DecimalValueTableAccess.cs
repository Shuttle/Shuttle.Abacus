using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess.Definitions
{
    public class DecimalValueTableAccess
    {
        private const string TableName = "DecimalValue";

        public static IQuery Add(DecimalTable decimalTable, DecimalValue item)
        {
            return InsertBuilder.Insert()
                .AddParameterValue(DecimalValueColumns.Id, item.Id)
                .Add(DecimalValueColumns.DecimalTableId).WithValue(decimalTable.Id)
                .AddParameterValue(DecimalValueColumns.ColumnIndex, item.Column)
                .AddParameterValue(DecimalValueColumns.RowIndex, item.Row)
                .AddParameterValue(DecimalValueColumns.DecimalValue, item.Value)
                .Into(TableName);
        }

        public static IQuery Remove(DecimalValue item)
        {
            return RawQuery.Create("delete from TABLE where Id = @Id").AddParameterValue(DecimalValueColumns.Id, item.Id);
        }

        public static IQuery Get(Guid id)
        {
            return RawQuery.Create(@"
select
                Id,
                DecimalTableId,
                ColumnIndex,
                RowIndex,
                DecimalValue,
                .AddParameterValue(DecimalValueColumns.Id, id)
                .From(TableName);
        }

        public static IQuery AllForDecimalTable(Guid decimalTableId)
        {
            return RawQuery.Create(@"
select
                Id,
                DecimalTableId,
                ColumnIndex,
                RowIndex,
                DecimalValue,
                .AddParameterValue(DecimalValueColumns.DecimalTableId, decimalTableId)
                .From(TableName);
        }

        public static IQuery RemoveAllForDecimalTable(Guid decimalTableId)
        {
            return DeleteBuilder.AddParameterValue(DecimalValueColumns.DecimalTableId, decimalTableId).From(TableName);
        }

        public static IQuery RemoveConstraintsForDecimalTable(Guid decimalTableId)
        {
            return
                RawQuery.Create(
                    "delete from [Constraint] from [Constraint] c inner join [DecimalValue] dv on (dv.DecimalValueId = c.OwnerId) where dv.DecimalTableId = @DecimalTableId")
                    .AddParameterValue(DecimalValueColumns.DecimalTableId, decimalTableId);
        }
    }
}