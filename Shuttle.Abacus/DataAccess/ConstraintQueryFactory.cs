using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public class ConstraintQueryFactory : IConstraintQueryFactory
    {
        private string SelectClause = @"
select
    OwnerName,
    OwnerId,
    ArgumentId,
    ArgumentName,
    Name,
    Answer,
    AnswerType,
    Description,
    SequenceNumber
from
    [Constraint]
";

        public IQuery All(Guid ownerId)
        {
            return RawQuery.Create(string.Concat(SelectClause, @"
where
    OwnerId = @OwnerId
order by
    SequenceNumber
"))
                .AddParameterValue(ConstraintColumns.OwnerId, ownerId);
        }

        public IQuery Remove(Guid ownerId)
        {
            return RawQuery.Create("delete from [Constraint] where OwnerId = @OwnerId")
                .AddParameterValue(ConstraintColumns.OwnerId, ownerId);
        }

        public IQuery Add(IConstraintOwner owner, OwnedConstraint constraint)
        {
            Guard.AgainstNull(owner, "owner");
            Guard.AgainstNull(constraint, "constraint");

            return RawQuery.Create(@"
insert into Constraint
(
    OwnerName,
    OwnerId,
    ArgumentId,
    Name,
    Answer,
    SequenceNumber
)
values
(
    @OwnerName,
    @OwnerId,
    @ArgumentId,
    @Name,
    @Answer,
    @SequenceNumber
)")
                .AddParameterValue(ConstraintColumns.OwnerName, owner.OwnerName)
                .AddParameterValue(ConstraintColumns.OwnerId, owner.Id)
                .AddParameterValue(ConstraintColumns.Name, constraint.Name)
                .AddParameterValue(ConstraintColumns.ArgumentId, constraint.ArgumentId)
                .AddParameterValue(ConstraintColumns.Answer, constraint.Answer)
                .AddParameterValue(ConstraintColumns.SequenceNumber, constraint.SequenceNumber);
        }
    }
}