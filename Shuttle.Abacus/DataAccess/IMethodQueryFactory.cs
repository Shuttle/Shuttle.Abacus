using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface IMethodQueryFactory
    {
        IQuery All();
        IQuery Get(Guid id);
        IQuery Add(Method item);
        IQuery Remove(Method item);
        IQuery Save(Method item);
        IQuery Get(string name);
    }
}