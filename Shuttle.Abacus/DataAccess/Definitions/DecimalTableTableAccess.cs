using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess.Definitions
{
    public class DecimalTableTableAccess
    {
        private const string TableName = "DecimalTable";

        public static IQuery Add(DecimalTable item)
        {
            return InsertBuilder.Insert()
                .AddParameterValue(DecimalTableColumns.Id, item.Id)
                .AddParameterValue(DecimalTableColumns.Name, item.Name)
                .AddParameterValue(DecimalTableColumns.RowArgumentId, item.RowArgumentId)
                .AddParameterValue(DecimalTableColumns.ColumnArgumentId, item.ColumnArgumentId)
                .Into(TableName);
        }

        public static IQuery Remove(DecimalTable item)
        {
            return RawQuery.Create("delete from TABLE where Id = @Id").AddParameterValue(DecimalTableColumns.Id, item.Id);
        }

        public static IQuery Get(Guid id)
        {
            return RawQuery.Create(@"
select
                Id,
                Name,
                RowArgumentId,
                ColumnArgumentId,
                .AddParameterValue(DecimalTableColumns.Id, id)
                .From(TableName);
        }

        public static IQuery Save(DecimalTable item)
        {
            return UpdateBuilder.Update(TableName)
                .Set(DecimalTableColumns.Name).ToValue(item.Name)
                .Set(DecimalTableColumns.RowArgumentId).ToValue(item.RowArgumentId)
                .Set(DecimalTableColumns.ColumnArgumentId).ToValue(item.ColumnArgumentId)
                .AddParameterValue(DecimalTableColumns.Id).HasValue(item.Id);
        }

        public static IQuery All()
        {
            return RawQuery.Create(@"
select
                Id,
                Name,
                RowArgumentId,
                ColumnArgumentId,
                .From(TableName);
        }
    }
}