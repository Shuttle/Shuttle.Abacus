using System.Collections.Generic;

namespace Shuttle.Abacus.Domain
{
    public interface IMatrixRepository
    {
        IEnumerable<Matrix> All();
    }
}