using System;
using Abacus.Domain;

namespace Abacus.Data
{
    public static class ConstraintTableAccess
    {
        public const string TableName = "Constraint";

        public static IQuery RemoveForOwner(IConstraintOwner owner)
        {
            return DeleteBuilder.Where(ConstraintColumns.OwnerId).EqualTo(owner.Id).From(TableName);
        }

        public static IQuery Add(IConstraintOwner owner, IConstraint constraint, int sequence)
        {
            return InsertBuilder
                .Insert()
                .Add(ConstraintColumns.OwnerName).WithValue(owner.OwnerName)
                .Add(ConstraintColumns.OwnerId).WithValue(owner.Id)
                .Add(ConstraintColumns.Name).WithValue(constraint.Name)
                .Add(ConstraintColumns.ArgumentId).WithValue(constraint.ArgumentId)
                .Add(ConstraintColumns.ArgumentName).WithValue(constraint.ArgumentAnswer.ArgumentName)
                .Add(ConstraintColumns.AnswerType).WithValue(constraint.ArgumentAnswer.AnswerType)
                .Add(ConstraintColumns.Answer).WithValue(constraint.ArgumentAnswer.AnswerString)
                .Add(ConstraintColumns.Description).WithValue(constraint.Description())
                .Add(ConstraintColumns.SequenceNumber).WithValue(sequence)
                .Into(TableName);
        }

        public static IQuery AllForOwner(Guid id)
        {
            return SelectBuilder
                .Select(ConstraintColumns.Name)
                .With(ConstraintColumns.ArgumentId)
                .With(ConstraintColumns.ArgumentName)
                .With(ConstraintColumns.Answer)
                .With(ConstraintColumns.AnswerType)
                .Where(ConstraintColumns.OwnerId).EqualTo(id)
                .OrderBy(ConstraintColumns.SequenceNumber).Ascending()
                .From(TableName);
        }

        public static IQuery SetArgumentName(Guid argumentId, string argumentName)
        {
            return UpdateBuilder.Update(TableName)
                .Set(ConstraintColumns.ArgumentName).ToValue(argumentName)
                .Where(ConstraintColumns.ArgumentId).HasValue(argumentId);
        }

        public static IQuery SetArgumentAnswerType(Guid argumentId, string answerType)
        {
            return UpdateBuilder.Update(TableName)
                .Set(ConstraintColumns.AnswerType).ToValue(answerType)
                .Where(ConstraintColumns.ArgumentId).HasValue(argumentId);
        }
    }
}