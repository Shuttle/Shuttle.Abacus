namespace Shuttle.Abacus;

public interface ITestQuery : IQuery<Query.Test, Query.Test.Specification>
{
    Task<IEnumerable<Query.Test.Argument>> ArgumentsAsync(Guid testId, CancellationToken cancellationToken = default);
}
