using System;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface IConstraintQueryFactory
    {
        IQuery All(Guid formulaId);
        IQuery Remove(Guid formulaId);
    }
}