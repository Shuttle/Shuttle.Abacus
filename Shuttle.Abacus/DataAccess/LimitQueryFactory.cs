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

        public IQuery Add(ILimitOwner owner, Limit item)
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
                .AddParameterValue(LimitColumns.Id, item.Id)
                .AddParameterValue(LimitColumns.OwnerName, owner.OwnerName)
                .AddParameterValue(LimitColumns.OwnerId, owner.Id)
                .AddParameterValue(LimitColumns.Name, item.Name)
                .AddParameterValue(LimitColumns.Type, item.Type);
        }

        public IQuery Remove(Limit item)
        {
            return RawQuery.Create("delete from Limit where LimitId = @LimitId")
                .AddParameterValue(LimitColumns.Id, item.Id);
        }

        public IQuery Save(Limit item)
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
                .AddParameterValue(LimitColumns.Name, item.Name)
                .AddParameterValue(LimitColumns.Type, item.Type)
                .AddParameterValue(LimitColumns.Id, item.Id);
        }
    }
}