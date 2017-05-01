using System;

namespace Shuttle.Abacus.Domain
{
    public interface ILimitRepository : IRepository<Limit>
    {
        void Save(Limit limit);
        void Add(string ownerName, Guid ownerId, Limit limit);
    }
}