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
    ArgumentName,
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
            throw new NotImplementedException();
//            return RawQuery.Create(string.Concat(SelectClause, @"
//where
//    OwnerId = @OwnerId
//order by
//    SequenceNumber
//"))
//                .AddParameterValue(FormulaColumns.ConstraintColumns.OwnerId, ownerId);
        }

        public IQuery Remove(Guid ownerId)
        {
            throw new NotImplementedException();
            //return RawQuery.Create("delete from [Constraint] where OwnerId = @OwnerId")
            //    .AddParameterValue(FormulaColumns.ConstraintColumns.OwnerId, ownerId);
        }

//        public IQuery Add(IConstraintOwner owner, FormulaConstraint constraint)
//        {
//            Guard.AgainstNull(owner, "owner");
//            Guard.AgainstNull(constraint, "constraint");

//            return RawQuery.Create(@"
//insert into Constraint
//(
//    OwnerName,
//    OwnerId,
//    ArgumentName,
//    Name,
//    Answer,
//    SequenceNumber
//)
//values
//(
//    @OwnerName,
//    @OwnerId,
//    @ArgumentName,
//    @Name,
//    @Answer,
//    @SequenceNumber
//)")
//                .AddParameterValue(ConstraintColumns.OwnerName, owner.OwnerName)
//                .AddParameterValue(ConstraintColumns.OwnerId, owner.Id)
//                .AddParameterValue(ConstraintColumns.Name, constraint.Name)
//                .AddParameterValue(ConstraintColumns.ArgumentId, constraint.ArgumentId)
//                .AddParameterValue(ConstraintColumns.Answer, constraint.Answer)
//                .AddParameterValue(ConstraintColumns.SequenceNumber, constraint.SequenceNumber);
//        }
    }
}