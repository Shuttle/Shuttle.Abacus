using System.Collections.Generic;

namespace Shuttle.Abacus.Domain
{
    public interface IArgumentRepository
    {
        IEnumerable<Argument> All();
    }
}