using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface ILimitQueryFactory
    {
        IQuery AllForOwner(Guid ownerId);
        IQuery Get(Guid limitId);
        IQuery Add(string ownerName, Guid ownerId, Limit limit);
        IQuery Remove(Guid id);
        IQuery Save(Limit limit);
    }
}