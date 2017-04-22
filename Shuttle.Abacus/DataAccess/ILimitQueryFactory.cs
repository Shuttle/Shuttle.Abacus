using System;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface ILimitQueryFactory
    {
        IQuery AllForOwner(Guid ownerId);
        IQuery Get(Guid limitId);
    }
}