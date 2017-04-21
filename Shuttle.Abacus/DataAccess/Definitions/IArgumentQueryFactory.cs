using System;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess.Definitions
{
    public interface IArgumentQueryFactory
    {
        IQuery All();
        IQuery Get(Guid id);
        IQuery GetRestrictedAnswer(Guid id);
    }
}