using System;
using Abacus.Domain;

namespace Abacus.Data
{
    public static class LimitTableAccess
    {
        private const string TableName = "Limit";

        public static IQuery Add(ILimitOwner owner, Limit item)
        {
            return InsertBuilder.Insert()
                .Add(LimitColumns.Id).WithValue(item.Id)
                .Add(LimitColumns.OwnerName).WithValue(owner.OwnerName)
                .Add(LimitColumns.OwnerId).WithValue(owner.Id)
                .Add(LimitColumns.Name).WithValue(item.Name)
                .Add(LimitColumns.Type).WithValue(item.Type)
                .Into(TableName);
        }

        public static IQuery Remove(Limit item)
        {
            return DeleteBuilder.Where(LimitColumns.Id).EqualTo(item.Id).From(TableName);
        }

        public static IQuery Get(Guid id)
        {
            return Get()
                .Where(LimitColumns.Id).EqualTo(id)
                .From(TableName);
        }

        private static ISelectBuilderSelect Get()
        {
            return SelectBuilder
                .Select(LimitColumns.Id)
                .With(LimitColumns.OwnerName)
                .With(LimitColumns.OwnerId)
                .With(LimitColumns.Name)
                .With(LimitColumns.Type);
        }

        public static IQuery Save(Limit item)
        {
            return UpdateBuilder.Update(TableName)
                .Set(LimitColumns.Name).ToValue(item.Name)
                .Set(LimitColumns.Type).ToValue(item.Type)
                .Where(LimitColumns.Id).HasValue(item.Id);
        }

        public static IQuery AllForOwner(Guid ownerId)
        {
            return Get()
               .Where(LimitColumns.OwnerId).EqualTo(ownerId)
               .From(TableName);
        }
    }
}
