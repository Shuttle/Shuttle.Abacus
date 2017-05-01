using System;

namespace Shuttle.Abacus.Domain
{
    public interface IFormulaRepository : IRepository<Formula>
    {
        void Add(IFormulaOwner owner, Formula formula);
        void Save(Formula item);
        void SaveOrdered(IFormulaOwner owner);
        void Add(string ownerName, Guid ownerId, Formula formula);
    }
}
