using System;
using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface IFormulaRepository : IRepository<Formula>
    {
        void Add(IFormulaOwner owner, Formula formula);
        void Save(Formula item);
        void SaveOrdered(IFormulaOwner owner);
        IEnumerable<Formula> AllForOwner(Guid ownerId);
    }
}
