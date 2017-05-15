using System;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Events.Formula.v1;
using Shuttle.Core.Data;
using Shuttle.Recall;

namespace Shuttle.Abacus.DataAccess
{
    public interface IFormulaQueryFactory
    {
        IQuery Operations(Guid id);
        IQuery Get(Guid id);
        IQuery Add(Formula formula);
        IQuery RemoveOperations(Guid formulaId);
        IQuery AddOperation(Guid formulaId, int sequenceNumber, string operation, string valueSource, string valueSelection);
        IQuery Save(Formula item);
        IQuery All();
        IQuery AddConstraint(Formula formula, FormulaConstraint constraint);
        IQuery RemoveConstraints(Formula formula);
        IQuery Constraints(Guid id);
        IQuery Registered(Guid formulaId, string name);
        IQuery Remove(Guid formulaId);
        IQuery Renamed(Guid formulaId, string name);
    }
}