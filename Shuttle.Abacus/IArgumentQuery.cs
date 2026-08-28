namespace Shuttle.Abacus;

public interface IArgumentQuery : IQuery<Query.Argument, Query.Argument.Specification>
{
    Task<IEnumerable<Query.Argument.Value>> ValuesAsync(Guid argumentId, CancellationToken cancellationToken = default);
}
