using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface IFormulaRepository
    {
        IEnumerable<Formula> All();
    }
}