using System.Collections.Generic;

namespace Shuttle.Abacus.Shell.Core.Binding
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
