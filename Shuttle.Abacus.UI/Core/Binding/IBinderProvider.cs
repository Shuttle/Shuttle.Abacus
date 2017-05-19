namespace Shuttle.Abacus.Shell.Core.Binding
{
    public interface IBinderProvider
    {
        IBinder<T> GetBinderFor<T>();
    }
}
