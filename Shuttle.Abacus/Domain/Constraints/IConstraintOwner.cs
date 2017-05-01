using System.Collections.Generic;

namespace Shuttle.Abacus.Domain
{
    public interface IConstraintOwner : IOwner
    {
        IEnumerable<OwnedConstraint> Constraints { get; }
        IConstraintOwner AddConstraint(IConstraint constraint);
        void AddConstraint(OwnedConstraint item);
    }
}
