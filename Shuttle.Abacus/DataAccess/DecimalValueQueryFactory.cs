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
    MatrixElement
from
    MatrixElement
";

        public static IQuery Add(Matrix matrix, MatrixElement item)
        {
            return RawQuery.Create(@"
insert into MatrixElement
(
    DecimalValueId,
    DecimalTableId,
    ColumnIndex,
    RowIndex,
    MatrixElement
)
values
(
    @DecimalValueId,
    @DecimalTableId,
    @ColumnIndex,
    @RowIndex,
    @MatrixElement
)")
                .AddParameterValue(DecimalValueColumns.Id, item.Id)
                .AddParameterValue(DecimalValueColumns.DecimalTableId, matrix.Id)
                .AddParameterValue(DecimalValueColumns.ColumnIndex, item.Column)
                .AddParameterValue(DecimalValueColumns.RowIndex, item.Row)
                .AddParameterValue(DecimalValueColumns.DecimalValue, item.Value);
        }

        public static IQuery Remove(MatrixElement item)
        {
            return RawQuery.Create("delete from MatrixElement where DecimalValueId = @DecimalValueId").AddParameterValue(DecimalValueColumns.Id, item.Id);
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
            return RawQuery.Create("delete from MatrixElement where DecimalTableId = @DecimalTableId").AddParameterValue(DecimalValueColumns.DecimalTableId, decimalTableId);
        }

        public static IQuery RemoveConstraintsForDecimalTable(Guid decimalTableId)
        {
            return
                RawQuery.Create(
                    "delete from [Constraint] from [Constraint] c inner join [MatrixElement] dv on (dv.DecimalValueId = c.OwnerId) where dv.DecimalTableId = @DecimalTableId")
                    .AddParameterValue(DecimalValueColumns.DecimalTableId, decimalTableId);
        }
    }
}