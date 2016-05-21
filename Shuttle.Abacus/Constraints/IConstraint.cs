using System;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public interface IConstraint : ISpecification<IMethodContext>
    {
        string Name { get; }
        string Text { get; }
        ArgumentAnswer ArgumentAnswer { get; }
        Guid ArgumentId { get; }
        string Description();
        IConstraint Copy();
    }
}
