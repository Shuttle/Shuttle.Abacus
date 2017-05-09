using System.Collections.Generic;

namespace Shuttle.Abacus.Domain
{
    public interface IConstraintOwnerx : IOwner
    {
        IEnumerable<FormulaConstraint> Constraints { get; }
        IConstraintOwnerx AddConstraint(IConstraint constraint);
        void AddConstraint(FormulaConstraint item);
    }
}
