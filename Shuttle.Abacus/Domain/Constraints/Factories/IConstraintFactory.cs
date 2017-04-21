using System;

namespace Shuttle.Abacus
{
    public interface IConstraintFactory : IFactory
    {
        IConstraint Create(Guid argumentId, ArgumentAnswer argumentAnswer);

        string Text { get; }
    }
}
