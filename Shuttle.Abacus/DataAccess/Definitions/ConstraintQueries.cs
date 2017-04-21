using System;

namespace Shuttle.Abacus.DataAccess.Definitions
{
    public static class ConstraintQueries
    {
        

        public IQuery AllForOwner(Guid ownerId)
        {
            return RawQuery.Create(@"
select
                Description,
                .AddParameterValue(ConstraintColumns.OwnerId, ownerId)
                .OrderBy(ConstraintColumns.SequenceNumber).Ascending()
                .From(TableName);
        }

        public IQuery DTOsForOwner(Guid ownerId)
        {
            return RawQuery.Create(@"
select
                Name,
                ArgumentId,
                ArgumentName,
                Answer,
                .AddParameterValue(ConstraintColumns.OwnerId, ownerId)
                .OrderBy(ConstraintColumns.SequenceNumber).Ascending()
                .From(TableName);
        }
    }
}
