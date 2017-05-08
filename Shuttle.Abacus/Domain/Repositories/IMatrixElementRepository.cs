using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.Domain
{
    public interface IMatrixElementRepository : IRepository<MatrixElement>
    {
        void Add(Matrix matrix, MatrixElement matrixElement);
        IEnumerable<MatrixElement> AllForDecimalTable(Matrix matrix);
        void RemoveAllForDecimalTable(Guid decimalTableId);
    }
}
