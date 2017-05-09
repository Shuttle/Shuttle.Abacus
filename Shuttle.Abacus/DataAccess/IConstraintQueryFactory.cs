using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface IConstraintQueryFactory
    {
        IQuery All(Guid ownerId);
        IQuery Remove(Guid ownerId);
        //IQuery Add(IConstraintOwner owner, FormulaConstraint constraint1);
    }
}