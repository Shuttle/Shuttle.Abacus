using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface IFormulaQueryFactory
    {
        IQuery AllForOwner(Guid ownerId);
        IQuery GetOperations(Guid id);
        IQuery Get(Guid id);
        IQuery Add(IFormulaOwner owner, Formula item);
        IQuery Remove(Formula item);
        IQuery RemoveOperations(Formula formula);
        IQuery AddOperation(Formula formula, FormulaOperation operation, int sequenceNumber);
        IQuery Save(Formula item);
        IQuery SetSequenceNumber(Formula formula, int sequence);
    }
}