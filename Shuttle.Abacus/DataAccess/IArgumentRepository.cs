using System.Collections.Generic;
using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.DataAccess
{
    public interface IArgumentRepository
    {
        IEnumerable<Argument> All();
    }
}