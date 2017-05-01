using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.Domain
{
    public interface IConstraintRepository
    {
        IEnumerable<IConstraint> AllForOwner(Guid ownerId);
    }
}
