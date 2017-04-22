using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface IConstraintQueryFactory
    {
        IQuery AllForOwner(Guid ownerId);
        IQuery RemoveForOwner(IConstraintOwner owner);
        IQuery Add(IConstraintOwner owner, IConstraint constraint, int sequence);
        IQuery SetArgumentName(Guid argumentId, string argumentName);
        IQuery SetArgumentAnswerType(Guid argumentId, string answerType);
    }
}