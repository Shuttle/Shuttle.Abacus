using System;

namespace Shuttle.Abacus.Domain
{
    public interface IArgumentRepository : IRepository<Argument>
    {
        void Save(Argument argument);
        Argument Find(Guid id);
    }
}
