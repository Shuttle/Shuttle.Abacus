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
        void ConstraintAdded(Guid matrixId, string axis, int index, Guid id, string comparison, string value);
        void ElementAdded(Guid matrixId, int column, int row, Guid id, string value);
    }
}