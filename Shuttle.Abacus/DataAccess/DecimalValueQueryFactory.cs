using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class DecimalValueQueryFactory
    {
        private static string SelectClause = @"
select
    DecimalValueId,
    DecimalTableId,
    ColumnIndex,
    RowIndex,
    DecimalValue
from
    DecimalValue
";

        public static IQuery Add(DecimalTable decimalTable, DecimalValue item)
        {
            return RawQuery.Create(@"
insert into DecimalValue
(
    DecimalValueId,
    DecimalTableId,
    ColumnIndex,
    RowIndex,
    DecimalValue
)
values
(
    @DecimalValueId,
    @DecimalTableId,
    @ColumnIndex,
    @RowIndex,
    @DecimalValue
)")
                .AddParameterValue(DecimalValueColumns.Id, item.Id)
                .AddParameterValue(DecimalValueColumns.DecimalTableId, decimalTable.Id)
                .AddParameterValue(DecimalValueColumns.ColumnIndex, item.Column)
                .AddParameterValue(DecimalValueColumns.RowIndex, item.Row)
                .AddParameterValue(DecimalValueColumns.DecimalValue, item.Value);
        }

        public static IQuery Remove(DecimalValue item)
        {
            return RawQuery.Create("delete from DecimalValue where Id = @Id").AddParameterValue(DecimalValueColumns.Id, item.Id);
        }

        public static IQuery Get(Guid id)
        {
            return RawQuery.Create(string.Concat(SelectClause, @"
where
    DecimalValueId = @DecimalValueId
"))
                .AddParameterValue(DecimalValueColumns.Id, id);
        }

        public static IQuery AllForDecimalTable(Guid decimalTableId)
        {
            return RawQuery.Create(string.Concat(SelectClause, @"
where
    DecimalTableId = @DecimalTableId
"))
                .AddParameterValue(DecimalValueColumns.DecimalTableId, decimalTableId);
        }

        public static IQuery RemoveAllForDecimalTable(Guid decimalTableId)
        {
            return RawQuery.Create("delete from DecimalValue where DecimalTableId = @DecimalTableId").AddParameterValue(DecimalValueColumns.DecimalTableId, decimalTableId);
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