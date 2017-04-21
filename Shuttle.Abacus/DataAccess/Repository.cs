using System;
using Abacus.Domain;
using Abacus.Infrastructure;
using Abacus.Localisation;

namespace Abacus.Data
{
    public abstract class Repository<T> : IRepository<T>
    {
        private static readonly string TypeName = typeof(T).Name;

        public abstract void Add(T item);
        public abstract void Remove(T item);
        public abstract T Get(Guid id);

        public TCast Get<TCast>(Guid id) where TCast : class, IEntity
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
