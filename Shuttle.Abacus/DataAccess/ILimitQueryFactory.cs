using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface ILimitQueryFactory
    {
        IQuery AllForOwner(Guid ownerId);
        IQuery Get(Guid limitId);
        IQuery Add(ILimitOwner owner, Limit item);
        IQuery Remove(Limit item);
        IQuery Save(Limit item);
    }
}