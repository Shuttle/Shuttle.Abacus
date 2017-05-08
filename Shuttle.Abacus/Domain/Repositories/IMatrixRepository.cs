using System.Collections.Generic;

namespace Shuttle.Abacus.Domain
{
    public interface IMatrixRepository : IRepository<Matrix>
    {
        IEnumerable<Matrix> All();
        void Save(Matrix matrix);
    }
}
