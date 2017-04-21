using System;
using System.Collections.Generic;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class BinderProvider : IBinderProvider
    {
        private readonly IDictionary<Type, object> binders = new Dictionary<Type, object>();

        public BinderProvider()
        {
            foreach (var binder in DependencyResolver.Resolver.ResolveAll<IBinder>())
            {
                binders.Add(binder.ForType, binder);
            }
        }

        public IBinder<T> GetBinderFor<T>()
        {
            if (!binders.ContainsKey(typeof(T)))
            {
                return null;
            }

            return binders[typeof (T)] as IBinder<T>;
        }
    }
}
