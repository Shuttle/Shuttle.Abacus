using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess.Definitions
{
    public static class LimitTableAccess
    {
        private const string TableName = "Limit";

        public static IQuery Add(ILimitOwner owner, Limit item)
        {
            return InsertBuilder.Insert()
                .AddParameterValue(LimitColumns.Id, item.Id)
                .Add(LimitColumns.OwnerName).WithValue(owner.OwnerName)
                .Add(LimitColumns.OwnerId).WithValue(owner.Id)
                .AddParameterValue(LimitColumns.Name, item.Name)
                .AddParameterValue(LimitColumns.Type, item.Type)
                .Into(TableName);
        }

        public static IQuery Remove(Limit item)
        {
            return RawQuery.Create("delete from TABLE where Id = @Id").AddParameterValue(LimitColumns.Id, item.Id);
        }

        public static IQuery Get(Guid id)
        {
            return Get()
                .AddParameterValue(LimitColumns.Id, id)
                .From(TableName);
        }

        private static ISelectBuilderSelect Get()
        {
            return RawQuery.Create(@"
select
                Id,
                OwnerName,
                OwnerId,
                Name,
                Type,;
        }

        public static IQuery Save(Limit item)
        {
            return UpdateBuilder.Update(TableName)
                .Set(LimitColumns.Name).ToValue(item.Name)
                .Set(LimitColumns.Type).ToValue(item.Type)
                .AddParameterValue(LimitColumns.Id).HasValue(item.Id);
        }

        public static IQuery AllForOwner(Guid ownerId)
        {
            return Get()
               .AddParameterValue(LimitColumns.OwnerId, ownerId)
               .From(TableName);
        }
    }
}
