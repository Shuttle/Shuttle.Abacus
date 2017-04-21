using System;
using Abacus.Domain;

namespace Abacus.Data
{
    public class DecimalTableTableAccess
    {
        private const string TableName = "DecimalTable";

        public static IQuery Add(DecimalTable item)
        {
            return InsertBuilder.Insert()
                .Add(DecimalTableColumns.Id).WithValue(item.Id)
                .Add(DecimalTableColumns.Name).WithValue(item.Name)
                .Add(DecimalTableColumns.RowArgumentId).WithValue(item.RowArgumentId)
                .Add(DecimalTableColumns.ColumnArgumentId).WithValue(item.ColumnArgumentId)
                .Into(TableName);
        }

        public static IQuery Remove(DecimalTable item)
        {
            return DeleteBuilder.Where(DecimalTableColumns.Id).EqualTo(item.Id).From(TableName);
        }

        public static IQuery Get(Guid id)
        {
            return SelectBuilder
                .Select(DecimalTableColumns.Id)
                .With(DecimalTableColumns.Name)
                .With(DecimalTableColumns.RowArgumentId)
                .With(DecimalTableColumns.ColumnArgumentId)
                .Where(DecimalTableColumns.Id).EqualTo(id)
                .From(TableName);
        }

        public static IQuery Save(DecimalTable item)
        {
            return UpdateBuilder.Update(TableName)
                .Set(DecimalTableColumns.Name).ToValue(item.Name)
                .Set(DecimalTableColumns.RowArgumentId).ToValue(item.RowArgumentId)
                .Set(DecimalTableColumns.ColumnArgumentId).ToValue(item.ColumnArgumentId)
                .Where(DecimalTableColumns.Id).HasValue(item.Id);
        }

        public static IQuery All()
        {
            return SelectBuilder
                .Select(DecimalTableColumns.Id)
                .With(DecimalTableColumns.Name)
                .With(DecimalTableColumns.RowArgumentId)
                .With(DecimalTableColumns.ColumnArgumentId)
                .From(TableName);
        }
    }
}