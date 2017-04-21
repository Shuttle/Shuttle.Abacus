using System.Collections.Generic;

namespace Shuttle.Abacus.Domain
{
    public interface ILimitOwner : IOwner
    {
        ILimitOwner AddLimit(Limit limit);

        IEnumerable<Limit> Limits { get; }
    }
}
