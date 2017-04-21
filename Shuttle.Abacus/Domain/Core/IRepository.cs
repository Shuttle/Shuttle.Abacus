using System;

namespace Shuttle.Abacus.Domain
{
    public interface IRepository
    {
        T Get<T>(Guid id) where T : class;

        string Name { get; }
    }

    public interface IRepository<T> : IRepository
    {
        void Add(T item);
        void Remove(T item);
        T Get(Guid id);
    }
}
