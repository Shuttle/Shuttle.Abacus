using System;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface IMatrixQueryFactory
    {
        IQuery All();
        IQuery Get(Guid id);
        IQuery Add(Guid id, string name, Guid? columnArgumentId, Guid rowArgumentId, string dataTypeName);
        IQuery Remove(Guid id);
        IQuery RemoveElements(Guid id);
        IQuery RemoveConstraints(Guid id);
        IQuery ConstraintAdded(Guid id, string axis, int index, string comparison, string value);
        IQuery ElementAdded(Guid id, int column, int row, string value);
        IQuery Search(MatrixSearchSpecification specification);
        IQuery Constaints(Guid id);
    }
}