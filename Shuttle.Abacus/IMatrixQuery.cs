namespace Shuttle.Abacus;

public interface IMatrixQuery : IQuery<Query.Matrix, Query.Matrix.Specification>
{
    Task<IEnumerable<Query.Matrix.Constraint>> ConstraintsAsync(Guid matrixId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Query.Matrix.Element>> ElementsAsync(Guid matrixId, CancellationToken cancellationToken = default);
}
