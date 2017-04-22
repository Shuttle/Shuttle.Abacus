using System;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface IFormulaQueryFactory
    {
        IQuery AllForOwner(Guid ownerId);
        IQuery GetOperations(Guid id);
        IQuery Get(Guid id);
    }
}