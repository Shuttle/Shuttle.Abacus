using System;
using Abacus.Domain;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public abstract class Repository<T> : Domain.IRepository<T>
    {
        private static readonly string TypeName = typeof(T).Name;

        public abstract void Add(T item);
        public abstract void Remove(T item);
        public abstract T Get(Guid id);

        public TCast Get<TCast>(Guid id) where TCast : class
        {
            var result = Get(id) as TCast;

            Guard.Against<InvalidCastException>(result == null,
                                                string.Format(Resources.NullSafeCasting, typeof (T).Name,
                                                              typeof (TCast).Name));

            return result;
        }

        public string Name
        {
            get { return TypeName; }
        }
    }
}
