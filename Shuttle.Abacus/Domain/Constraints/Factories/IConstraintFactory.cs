using System;

namespace Shuttle.Abacus.Domain
{
    public interface IConstraintFactory : IFactory
    {
        IConstraint Create(Guid argumentId, ArgumentAnswer argumentAnswer);

        string Text { get; }
    }
}
