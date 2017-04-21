using System;

namespace Abacus.Data
{
    public static class LimitQueries
    {
        private const string TableName = "Limit";

        public static ISelectQuery AllForOwner(Guid ownerId)
        {
            return SelectBuilder
                .Select(LimitColumns.Id)
                .With(LimitColumns.Name)
                .Where(LimitColumns.OwnerId).EqualTo(ownerId)
                .OrderBy(LimitColumns.Name).Ascending()
                .From(TableName);
        }

        public static ISelectQuery Get(Guid limitId)
        {
            return SelectBuilder
                .Select(LimitColumns.Id)
                .With(LimitColumns.Name)
                .With(LimitColumns.Type)
                .Where(LimitColumns.Id).EqualTo(limitId)
                .OrderBy(LimitColumns.Name).Ascending()
                .From(TableName);
        }
    }
}
