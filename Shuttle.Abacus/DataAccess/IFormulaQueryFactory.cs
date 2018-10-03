using System;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface IFormulaQueryFactory
    {
        IQuery Operations(Guid id);
        IQuery Get(Guid id);
        IQuery RemoveOperation(Guid operationId);
        IQuery RegisterOperation(Guid operationId, Guid formulaId, int sequenceNumber, string operation, string valueProviderName, string inputParameter);
        IQuery Search(FormulaSearchSpecification specification);
        IQuery RegisterConstraint(Guid constraintId, Guid formulaId, Guid argumentId, string comparison, string value);
        IQuery RemoveConstraint(Guid constraintId);
        IQuery Constraints(Guid id);
        IQuery Registered(Guid formulaId, string name);
        IQuery Remove(Guid formulaId);
        IQuery Rename(Guid formulaId, string name);
        IQuery RenumberOperations(Guid formulaId, int fromSequenceNumber);
    }
}