using Microsoft.EntityFrameworkCore;
using Shuttle.Contract;

namespace Shuttle.Abacus.SqlServer;

public class AlgorithmQuery(AbacusDbContext dbContext) : IAlgorithmQuery
{
    private readonly AbacusDbContext _dbContext = Guard.AgainstNull(dbContext);

    public async ValueTask<int> CountAsync(Query.Algorithm.Specification specification, CancellationToken cancellationToken = default)
    {
        return await GetQueryable(specification).CountAsync(cancellationToken);
    }

    public async Task<IEnumerable<Query.Algorithm>> SearchAsync(Query.Algorithm.Specification specification, CancellationToken cancellationToken = default)
    {
        var queryable = GetQueryable(specification).OrderBy(e => e.Name);

        if (specification.MaximumRows > 0)
        {
            queryable = (IOrderedQueryable<Models.Algorithm>)queryable.Take(specification.MaximumRows);
        }

        return (await queryable.ToListAsync(cancellationToken)).Select(e => new Query.Algorithm
        {
            Id = e.Id,
            Name = e.Name,
            MaximumAlgorithmName = e.MaximumAlgorithmName,
            MinimumAlgorithmName = e.MinimumAlgorithmName
        });
    }

    public async Task<IEnumerable<Query.Algorithm.Operation>> OperationsAsync(Guid algorithmId, CancellationToken cancellationToken = default)
    {
        return (await _dbContext.AlgorithmOperations.AsNoTracking()
                .Where(e => e.AlgorithmId == algorithmId)
                .OrderBy(e => e.SequenceNumber)
                .ToListAsync(cancellationToken))
            .Select(e => new Query.Algorithm.Operation
            {
                Id = e.Id,
                AlgorithmId = e.AlgorithmId,
                SequenceNumber = e.SequenceNumber,
                OperationName = e.Operation,
                ValueProviderName = e.ValueProviderName,
                InputParameter = e.InputParameter
            });
    }

    public async Task<IEnumerable<Query.Algorithm.Constraint>> ConstraintsAsync(Guid algorithmId, CancellationToken cancellationToken = default)
    {
        return (await _dbContext.AlgorithmConstraints.AsNoTracking()
                .Where(e => e.AlgorithmId == algorithmId)
                .ToListAsync(cancellationToken))
            .Select(e => new Query.Algorithm.Constraint
            {
                Id = e.Id,
                AlgorithmId = e.AlgorithmId,
                ArgumentId = e.ArgumentId,
                Comparison = e.Comparison,
                Value = e.Value
            });
    }

    private IQueryable<Models.Algorithm> GetQueryable(Query.Algorithm.Specification specification)
    {
        var queryable = _dbContext.Algorithms.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(specification.NameMatch))
        {
            queryable = queryable.Where(e => EF.Functions.Like(e.Name, $"%{specification.NameMatch}%"));
        }

        if (specification.HasIds)
        {
            queryable = queryable.Where(e => specification.Ids.Contains(e.Id));
        }

        if (specification.HasExcludedIds)
        {
            queryable = queryable.Where(e => !specification.ExcludedIds.Contains(e.Id));
        }

        return queryable;
    }
}
