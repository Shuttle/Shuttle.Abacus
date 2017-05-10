using System;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Events.Formula.v1;
using Shuttle.Core.Data;
using Shuttle.Recall;

namespace Shuttle.Abacus.DataAccess
{
    public interface IFormulaQueryFactory
    {
        IQuery GetOperations(Guid id);
        IQuery Get(Guid id);
        IQuery Add(Formula formula);
        IQuery Remove(Guid id);
        IQuery RemoveOperations(Formula formula);
        IQuery AddOperation(Formula formula, FormulaOperation operation, int sequenceNumber);
        IQuery Save(Formula item);
        IQuery All();
        IQuery AddConstraint(Formula formula, FormulaConstraint constraint);
        IQuery RemoveConstraints(Formula formula);
        IQuery GetConstraints(Guid id);
        IQuery Registered(PrimitiveEvent primitiveEvent, Registered registered);
        IQuery Removed(PrimitiveEvent primitiveEvent, Removed removed);
    }
}