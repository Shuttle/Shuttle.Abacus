using System.Collections.Generic;
using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.DataAccess
{
    public interface IFormulaRepository
    {
        IEnumerable<Formula> All();
    }
}