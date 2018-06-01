using System;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface IFormulaQueryFactory
    {
        IQuery Operations(Guid id);
        IQuery Get(Guid id);
        IQuery RemoveOperations(Guid formulaId);

        IQuery AddOperation(Guid formulaId, int sequenceNumber, string operation, string valueSource,
            string valueSelection);

        IQuery Save(Formula item);
        IQuery All();
        IQuery AddConstraint(Guid formulaId, int sequenceNumber, string argumentName, string comparison, string value);
        IQuery RemoveConstraints(Guid formulaId);
        IQuery Constraints(Guid id);
        IQuery Registered(Guid formulaId, string name);
        IQuery Remove(Guid formulaId);
        IQuery Renamed(Guid formulaId, string name);
    }
}