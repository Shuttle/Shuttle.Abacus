using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess.Definitions
{
    public static class MethodTableAccess
    {
        

        public static IQuery Add(Method item)
        {
            return InsertBuilder.Insert()
                .AddParameterValue(MethodColumns.Id, item.Id)
                .AddParameterValue(MethodColumns.Name, item.MethodName)
                .Into(TableName);
        }

        public static IQuery Remove(Method item)
        {
            return RawQuery.Create("delete from TABLE where Id = @Id").AddParameterValue(MethodColumns.Id, item.Id);
        }

        public static IQuery Get(Guid id)
        {
            return RawQuery.Create(@"
select
                Id,
                Name,
                .AddParameterValue(MethodColumns.Id, id)
                .From(TableName);
        }

        public static IQuery Save(Method item)
        {
            return UpdateBuilder.Update(TableName)
                .Set(MethodColumns.Name).ToValue(item.MethodName)
                .AddParameterValue(MethodColumns.Id).HasValue(item.Id);
        }

        public static IQuery Get(string methodName)
        {
            return RawQuery.Create(@"
select
                Id,
                Name,
                .AddParameterValue(MethodColumns.Name, methodName)
                .From(TableName);
        }
    }
}
