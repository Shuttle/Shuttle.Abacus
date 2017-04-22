using Abacus.Domain;

namespace Shuttle.Abacus.Domain
{
    public interface IOwner : IEntity
    {
        string OwnerName { get; }
    }
}