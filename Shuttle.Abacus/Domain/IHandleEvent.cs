namespace Abacus.Domain
{
    public interface IHandleEvent<in T> where T : class
    {
        void Handle(T args);
    }
}
