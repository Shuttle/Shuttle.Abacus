namespace Shuttle.Abacus;

public interface IFormulaQuery : IQuery<Query.Formula, Query.Formula.Specification>
{
    Task<IEnumerable<Query.Formula.Operation>> OperationsAsync(Guid formulaId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Query.Formula.Constraint>> ConstraintsAsync(Guid formulaId, CancellationToken cancellationToken = default);
}
