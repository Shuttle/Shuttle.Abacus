using System.Collections.Generic;

namespace Shuttle.Abacus.Domain
{
    public interface IFormulaRepository
    {
        IEnumerable<Formula> All();
    }
}