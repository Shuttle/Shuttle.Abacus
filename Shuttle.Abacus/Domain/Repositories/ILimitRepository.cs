using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.Domain
{
    public interface ILimitRepository :
        IRepository<Limit>
    {
        void Add(ILimitOwner owner, Limit limit);
        void Save(Limit limit);
        IEnumerable<Limit> AllForOwner(Guid ownerId);
    }
}
