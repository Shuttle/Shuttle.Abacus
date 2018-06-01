using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface IMatrixRepository
    {
        IEnumerable<Matrix> All();
    }
}