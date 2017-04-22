using System;

namespace Shuttle.Abacus.Domain
{
    public interface IOwner 
    {
        Guid Id { get; }
        string OwnerName { get; }
    }
}
