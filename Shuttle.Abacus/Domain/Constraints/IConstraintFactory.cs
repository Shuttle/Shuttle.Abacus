using System;

namespace Shuttle.Abacus.Domain
{
    public interface IConstraintFactory
    {
        IConstraint Create(string name, Guid argumentId, ArgumentAnswer argumentAnswer);
    }
}
