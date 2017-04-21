namespace Abacus.Domain
{
    public interface IHandleEvent<T> where T : IDomainEvent
    {
        void Handle(T args);
    }
}
