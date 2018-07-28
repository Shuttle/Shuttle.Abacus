using System;
using System.Collections.Generic;
using System.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface IMatrixQuery
    {
        IEnumerable<DataRow> Search(MatrixSearchSpecification specification);
        IEnumerable<DataRow> All();
        DataRow Get(Guid id);
        IEnumerable<DataRow> Constraints(Guid id);
        void Registered(Guid id, string name, Guid? columnArgumentId, Guid rowArgumentId, string dataTypeName);
        void ConstraintAdded(Guid id, string axis, int index, string comparison, string value);
        void ElementAdded(Guid id, int column, int row, string value);
    }
}