using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.Domain
{
    public interface ICalculationRepository :
        IRepository<Calculation>
    {
        void SaveOrdered(Method method);
        void Add(Method method, ICalculationOwner owner, Calculation entity);
        void Save(Calculation calculation);
        IEnumerable<Calculation> AllForOwner(Guid ownerId);
        void SaveOwnershipGraph(Method method);
    }
}
