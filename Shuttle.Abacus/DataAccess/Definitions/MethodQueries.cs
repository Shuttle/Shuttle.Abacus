using System;

namespace Abacus.Data
{
    public static class MethodQueries
    {
        public const string TableName = "Method";

        public static ISelectQuery All()
        {
            return SelectBuilder
                .Select(MethodColumns.Id)
                .With(MethodColumns.Name)
                .OrderBy(MethodColumns.Name).Ascending()
                .From(TableName);
        }

        public static ISelectQuery MethodName(Guid id)
        {
            return SelectBuilder
                .Select(MethodColumns.Id)
                .With(MethodColumns.Name)
                .Where(MethodColumns.Id).EqualTo(id)
                .From(TableName);
        }

        public static ISelectQuery Get(Guid id)
        {
            return SelectBuilder
                .Select(MethodColumns.Id)
                .With(MethodColumns.Name)
                .Where(MethodColumns.Id).EqualTo(id)
                .From(TableName);
        }
    }
}
