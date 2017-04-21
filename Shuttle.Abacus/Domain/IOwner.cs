namespace Abacus.Domain
{
    public interface IOwner : IEntity
    {
        string OwnerName { get; }
    }
}
