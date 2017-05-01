using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class LimitQueryFactory : ILimitQueryFactory
    {
        private readonly string SelectClause = @"
select
    LimitId,
    OwnerName,
    OwnerId,
    Name,
    Type
from
    Limit
";

        public IQuery AllForOwner(Guid ownerId)
        {
            return RawQuery.Create(string.Concat(SelectClause, @"
where
    OwnerId = @OwnerId
order by
    Name
"))
                .AddParameterValue(LimitColumns.OwnerId, ownerId);
        }

        public IQuery Get(Guid limitId)
        {
            return RawQuery.Create(string.Concat(SelectClause, @"
where
    LimitId = @LimitId
"))
                .AddParameterValue(LimitColumns.Id, limitId);
        }

        public IQuery Add(string ownerName, Guid ownerId, Limit limit)
        {
            return RawQuery.Create(@"
insert into Limit
(
    LimitId,
    OwnerName,
    OwnerId,
    Name,
    Type
)
values
(
    @LimitId,
    @OwnerName,
    @OwnerId,
    @Name,
    @Type
)")
                .AddParameterValue(LimitColumns.Id, limit.Id)
                .AddParameterValue(LimitColumns.OwnerName, ownerName)
                .AddParameterValue(LimitColumns.OwnerId, ownerId)
                .AddParameterValue(LimitColumns.Name, limit.Name)
                .AddParameterValue(LimitColumns.Type, limit.Type);
        }

        public IQuery Remove(Guid id)
        {
            return RawQuery.Create("delete from Limit where LimitId = @LimitId")
                .AddParameterValue(LimitColumns.Id, id);
        }

        public IQuery Save(Limit limit)
        {
            return RawQuery.Create(@"
update
    Limit
set
    Name = @Name,
    Type = @Type
where
    LimitId = @LimitId
")
                .AddParameterValue(LimitColumns.Name, limit.Name)
                .AddParameterValue(LimitColumns.Type, limit.Type)
                .AddParameterValue(LimitColumns.Id, limit.Id);
        }
    }
}