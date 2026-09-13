namespace Shuttle.Abacus;

public interface IAlgorithmQuery : IQuery<Query.Algorithm, Query.Algorithm.Specification>
{
    Task<IEnumerable<Query.Algorithm.Operation>> OperationsAsync(Guid algorithmId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Query.Algorithm.Constraint>> ConstraintsAsync(Guid algorithmId, CancellationToken cancellationToken = default);
}
