using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

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

        public IQuery AllForOwner(Guid ownerId)
        {
            return RawQuery.Create(string.Concat(SelectClause, @"
where
    OwnerId = @OwnerId
order by
    SequenceNumber
"))
                .AddParameterValue(ConstraintColumns.OwnerId, ownerId);
        }

        public IQuery RemoveForOwner(IConstraintOwner owner)
        {
            return RawQuery.Create("delete from [Constraint] where OwnerId = @OwnerId")
                .AddParameterValue(ConstraintColumns.OwnerId, owner.Id);
        }

        public IQuery Add(IConstraintOwner owner, IConstraint constraint, int sequence)
        {
            return RawQuery.Create(@"
insert into Constraint
(
    OwnerName,
    OwnerId,
    ArgumentId,
    ArgumentName,
    Name,
    Answer,
    AnswerType,
    Description,
    SequenceNumber
)
values
(
    @OwnerName,
    @OwnerId,
    @ArgumentId,
    @ArgumentName,
    @Name,
    @Answer,
    @AnswerType,
    @Description,
    @SequenceNumber
)")
                .AddParameterValue(ConstraintColumns.OwnerName, owner.OwnerName)
                .AddParameterValue(ConstraintColumns.OwnerId, owner.Id)
                .AddParameterValue(ConstraintColumns.Name, constraint.Name)
                .AddParameterValue(ConstraintColumns.ArgumentId, constraint.ArgumentId)
                .AddParameterValue(ConstraintColumns.ArgumentName, constraint.ArgumentAnswer.ArgumentName)
                .AddParameterValue(ConstraintColumns.AnswerType, constraint.ArgumentAnswer.AnswerType)
                .AddParameterValue(ConstraintColumns.Answer, constraint.ArgumentAnswer.AnswerString)
                .AddParameterValue(ConstraintColumns.Description, constraint.Description())
                .AddParameterValue(ConstraintColumns.SequenceNumber, sequence);
        }

        public IQuery SetArgumentName(Guid argumentId, string argumentName)
        {
            return RawQuery.Create(@"update [Constraint] set ArgumentName = @ArgumentName where ArgumentId = @ArgumentId")
                .AddParameterValue(ConstraintColumns.ArgumentName, argumentName)
                .AddParameterValue(ConstraintColumns.ArgumentId, argumentId);
        }

        public IQuery SetArgumentAnswerType(Guid argumentId, string answerType)
        {
            return RawQuery.Create(@"update [Constraint] set AnswerType = @AnswerType where ArgumentId = @ArgumentId")
                .AddParameterValue(ConstraintColumns.AnswerType, answerType)
                .AddParameterValue(ConstraintColumns.ArgumentId, argumentId);
        }
    }
}