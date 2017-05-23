using System.Collections.Generic;
using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus
{
    public interface ICachedArgumentProvider
    {
        IEnumerable<Argument> All();
        void Flush();
    }
}