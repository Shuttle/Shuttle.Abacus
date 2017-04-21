using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess.Definitions
{
    public static class ConstraintTableAccess
    {
        

        public static IQuery RemoveForOwner(IConstraintOwner owner)
        {
            return DeleteBuilder.AddParameterValue(ConstraintColumns.OwnerId, owner.Id).From(TableName);
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
            return RawQuery.Create(@"
select
                Name,
                ArgumentId,
                ArgumentName,
                Answer,
                AnswerType,
                .AddParameterValue(ConstraintColumns.OwnerId, id)
                .OrderBy(ConstraintColumns.SequenceNumber).Ascending()
                .From(TableName);
        }

        public static IQuery SetArgumentName(Guid argumentId, string argumentName)
        {
            return UpdateBuilder.Update(TableName)
                .Set(ConstraintColumns.ArgumentName).ToValue(argumentName)
                .AddParameterValue(ConstraintColumns.ArgumentId).HasValue(argumentId);
        }

        public static IQuery SetArgumentAnswerType(Guid argumentId, string answerType)
        {
            return UpdateBuilder.Update(TableName)
                .Set(ConstraintColumns.AnswerType).ToValue(answerType)
                .AddParameterValue(ConstraintColumns.ArgumentId).HasValue(argumentId);
        }
    }
}