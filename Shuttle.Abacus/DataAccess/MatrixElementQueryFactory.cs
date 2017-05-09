using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class MatrixElementQueryFactory
    {
        private static string SelectClause = @"
select
    DecimalValueId,
    MatrixId,
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
    MatrixId,
    ColumnIndex,
    RowIndex,
    MatrixElement
)
values
(
    @DecimalValueId,
    @MatrixId,
    @ColumnIndex,
    @RowIndex,
    @MatrixElement
)")
                .AddParameterValue(MatrixColumns.ElementColumns.Id, item.Id)
                .AddParameterValue(MatrixColumns.ElementColumns.MatrixId, matrix.Id)
                .AddParameterValue(MatrixColumns.ElementColumns.ColumnIndex, item.Column)
                .AddParameterValue(MatrixColumns.ElementColumns.RowIndex, item.Row)
                .AddParameterValue(MatrixColumns.ElementColumns.DecimalValue, item.Value);
        }

        public static IQuery Remove(MatrixElement item)
        {
            return RawQuery.Create("delete from MatrixElement where DecimalValueId = @DecimalValueId").AddParameterValue(MatrixColumns.ElementColumns.Id, item.Id);
        }

        public static IQuery Get(Guid id)
        {
            return RawQuery.Create(string.Concat(SelectClause, @"
where
    DecimalValueId = @DecimalValueId
"))
                .AddParameterValue(MatrixColumns.ElementColumns.Id, id);
        }

        public static IQuery AllForDecimalTable(Guid decimalTableId)
        {
            return RawQuery.Create(string.Concat(SelectClause, @"
where
    MatrixId = @MatrixId
"))
                .AddParameterValue(MatrixColumns.ElementColumns.MatrixId, decimalTableId);
        }

        public static IQuery RemoveAllForDecimalTable(Guid decimalTableId)
        {
            return RawQuery.Create("delete from MatrixElement where MatrixId = @MatrixId").AddParameterValue(MatrixColumns.ElementColumns.MatrixId, decimalTableId);
        }

        public static IQuery RemoveConstraintsForDecimalTable(Guid decimalTableId)
        {
            return
                RawQuery.Create(
                    "delete from [Constraint] from [Constraint] c inner join [MatrixElement] dv on (dv.DecimalValueId = c.OwnerId) where dv.MatrixId = @MatrixId")
                    .AddParameterValue(MatrixColumns.ElementColumns.MatrixId, decimalTableId);
        }
    }
}