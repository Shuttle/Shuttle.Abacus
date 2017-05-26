using System;

namespace Shuttle.Abacus.Domain
{
    public interface IMatrixRepository
    {
        void Add(Matrix item);
        void Remove(Guid id);
        Matrix Get(Guid id);
        void Save(Matrix matrix);
    }
}
