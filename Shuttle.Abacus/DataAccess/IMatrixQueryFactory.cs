using System;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface IMatrixQueryFactory
    {
        IQuery All();
        IQuery Get(Guid id);
        IQuery Add(Guid id, string name, string columnArgumentName, string rowArgumentName, string valueType);
        IQuery Remove(Guid id);
        IQuery RemoveElements(Guid id);
        IQuery RemoveConstraints(Guid id);
        IQuery ConstraintAdded(Guid id, int sequenceNumber, string axis, string comparison, string value);
        IQuery ElementAdded(Guid id, int column, int row, string value);
    }
}