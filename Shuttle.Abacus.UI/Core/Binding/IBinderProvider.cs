namespace Abacus.UI
{
    public interface IBinderProvider
    {
        IBinder<T> GetBinderFor<T>();
    }
}
