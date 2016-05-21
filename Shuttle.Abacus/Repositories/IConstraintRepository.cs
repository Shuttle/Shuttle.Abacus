using System;
using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface IConstraintRepository
    {
        void SaveForOwner(IConstraintOwner owner);

        IEnumerable<IConstraint> AllForOwner(Guid ownerId);
        void SetArgumentName(Guid argumentId, string argumentName);
        void SetArgumentAnswerType(Guid argumentId, string answerType);
    }
}
