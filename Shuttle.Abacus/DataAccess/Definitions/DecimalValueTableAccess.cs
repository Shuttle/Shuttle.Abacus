using System;
using Abacus.Domain;

namespace Abacus.Data
{
    public class DecimalValueTableAccess
    {
        private const string TableName = "DecimalValue";

        public static IQuery Add(DecimalTable decimalTable, DecimalValue item)
        {
            return InsertBuilder.Insert()
                .Add(DecimalValueColumns.Id).WithValue(item.Id)
                .Add(DecimalValueColumns.DecimalTableId).WithValue(decimalTable.Id)
                .Add(DecimalValueColumns.ColumnIndex).WithValue(item.Column)
                .Add(DecimalValueColumns.RowIndex).WithValue(item.Row)
                .Add(DecimalValueColumns.DecimalValue).WithValue(item.Value)
                .Into(TableName);
        }

        public static IQuery Remove(DecimalValue item)
        {
            return DeleteBuilder.Where(DecimalValueColumns.Id).EqualTo(item.Id).From(TableName);
        }

        public static IQuery Get(Guid id)
        {
            return SelectBuilder
                .Select(DecimalValueColumns.Id)
                .With(DecimalValueColumns.DecimalTableId)
                .With(DecimalValueColumns.ColumnIndex)
                .With(DecimalValueColumns.RowIndex)
                .With(DecimalValueColumns.DecimalValue)
                .Where(DecimalValueColumns.Id).EqualTo(id)
                .From(TableName);
        }

        public static IQuery AllForDecimalTable(Guid decimalTableId)
        {
            return SelectBuilder
                .Select(DecimalValueColumns.Id)
                .With(DecimalValueColumns.DecimalTableId)
                .With(DecimalValueColumns.ColumnIndex)
                .With(DecimalValueColumns.RowIndex)
                .With(DecimalValueColumns.DecimalValue)
                .Where(DecimalValueColumns.DecimalTableId).EqualTo(decimalTableId)
                .From(TableName);
        }

        public static IQuery RemoveAllForDecimalTable(Guid decimalTableId)
        {
            return DeleteBuilder.Where(DecimalValueColumns.DecimalTableId).EqualTo(decimalTableId).From(TableName);
        }

        public static IQuery RemoveConstraintsForDecimalTable(Guid decimalTableId)
        {
            return
                DynamicQuery.CreateFrom(
                    "delete from [Constraint] from [Constraint] c inner join [DecimalValue] dv on (dv.DecimalValueId = c.OwnerId) where dv.DecimalTableId = @DecimalTableId")
                    .AddParameterValue(DecimalValueColumns.DecimalTableId, decimalTableId);
        }
    }
}