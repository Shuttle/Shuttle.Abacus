using System;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public interface IConstraint : ISpecification<IMethodContext>
    {
        string Name { get; }
        string Text { get; }
        ValueType ValueType { get; }
        Guid ArgumentId { get; }
        string Description();
        IConstraint Copy();
    }
}
