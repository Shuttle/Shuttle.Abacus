using System;
using System.Collections.Generic;
using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.UI.Core.Binding
{
    public class BinderProvider : IBinderProvider
    {
        private readonly List<IBinder> _binders = new List<IBinder>();

        public BinderProvider()
        {
            _binders.Add(new ListViewBinder());
        }

        public IBinder<T> GetBinderFor<T>()
        {
            return _binders.Find(binder => binder.ForType == typeof(T)) as IBinder<T>;
        }
    }
}
