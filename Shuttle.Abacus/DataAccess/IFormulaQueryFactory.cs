using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

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
    }
}