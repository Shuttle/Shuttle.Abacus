using System;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class ConstraintQueryFactory : IConstraintQueryFactory
    {
        private readonly string SelectClause = @"
select
    FormulaId,
    SequenceNumber,
    ArgumentName,
    Comparison,
    Value
from
    FormulaConstraint
";

        public IQuery All(Guid formulaId)
        {
            return RawQuery.Create(string.Concat(SelectClause, @"
where
    FormulaId = @FormulaId
order by
    SequenceNumber
"))
                .AddParameterValue(FormulaColumns.FormulaId, formulaId);
        }

        public IQuery Remove(Guid ownerId)
        {
            throw new NotImplementedException();
            //return RawQuery.Create("delete from [Constraint] where OwnerId = @OwnerId")
            //    .AddParameterValue(FormulaColumns.ConstraintColumns.OwnerId, formulaId);
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