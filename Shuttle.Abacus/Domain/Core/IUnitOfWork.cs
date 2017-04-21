using System;

namespace Shuttle.Abacus.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IUnitOfWork Begin();
        void Commit();
        void WillUse<T>();
        void WillUseFullObjectGraph();
        void WillUseNothing();
        bool Uses<T>();
        T Get<T>(Guid id) where T : class;
        bool Contains(Guid id);
        void Register(object entity);
    }
}
