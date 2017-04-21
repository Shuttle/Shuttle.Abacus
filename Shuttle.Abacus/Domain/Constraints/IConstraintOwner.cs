using System.Collections.Generic;

namespace Shuttle.Abacus.Domain
{
    public interface IConstraintOwner : IOwner
    {
        IEnumerable<IConstraint> Constraints { get; }
        IConstraintOwner AddConstraint(IConstraint constraint);
    }
}
