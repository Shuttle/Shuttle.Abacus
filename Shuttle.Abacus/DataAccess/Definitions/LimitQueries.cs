using System;

namespace Shuttle.Abacus.DataAccess.Definitions
{
    public static class LimitQueries
    {
        private const string TableName = "Limit";

        public IQuery AllForOwner(Guid ownerId)
        {
            return RawQuery.Create(@"
select
                Id,
                Name,
                .AddParameterValue(LimitColumns.OwnerId, ownerId)
                .OrderBy(LimitColumns.Name).Ascending()
                .From(TableName);
        }

        public IQuery Get(Guid limitId)
        {
            return RawQuery.Create(@"
select
                Id,
                Name,
                Type,
                .AddParameterValue(LimitColumns.Id, limitId)
                .OrderBy(LimitColumns.Name).Ascending()
                .From(TableName);
        }
    }
}
