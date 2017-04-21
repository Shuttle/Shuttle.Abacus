using System;
using Abacus.Domain;

namespace Abacus.Data
{
    public static class MethodTableAccess
    {
        public const string TableName = "Method";

        public static IQuery Add(Method item)
        {
            return InsertBuilder.Insert()
                .Add(MethodColumns.Id).WithValue(item.Id)
                .Add(MethodColumns.Name).WithValue(item.MethodName)
                .Into(TableName);
        }

        public static IQuery Remove(Method item)
        {
            return DeleteBuilder.Where(MethodColumns.Id).EqualTo(item.Id).From(TableName);
        }

        public static IQuery Get(Guid id)
        {
            return SelectBuilder
                .Select(MethodColumns.Id)
                .With(MethodColumns.Name)
                .Where(MethodColumns.Id).EqualTo(id)
                .From(TableName);
        }

        public static IQuery Save(Method item)
        {
            return UpdateBuilder.Update(TableName)
                .Set(MethodColumns.Name).ToValue(item.MethodName)
                .Where(MethodColumns.Id).HasValue(item.Id);
        }

        public static IQuery Get(string methodName)
        {
            return SelectBuilder
                .Select(MethodColumns.Id)
                .With(MethodColumns.Name)
                .Where(MethodColumns.Name).EqualTo(methodName)
                .From(TableName);
        }
    }
}
