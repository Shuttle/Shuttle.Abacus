using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface IArgumentRepository
    {
        IEnumerable<Argument> All();
    }
}