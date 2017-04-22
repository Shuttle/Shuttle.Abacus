namespace Shuttle.Abacus.UI.Core.Binding
{
    public interface IBinderProvider
    {
        IBinder<T> GetBinderFor<T>();
    }
}
