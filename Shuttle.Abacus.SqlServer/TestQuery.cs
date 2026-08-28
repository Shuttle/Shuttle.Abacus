using Microsoft.EntityFrameworkCore;
using Shuttle.Contract;

namespace Shuttle.Abacus.SqlServer;

public class TestQuery(AbacusDbContext dbContext) : ITestQuery
{
    private readonly AbacusDbContext _dbContext = Guard.AgainstNull(dbContext);

    public async ValueTask<int> CountAsync(Query.Test.Specification specification, CancellationToken cancellationToken = default)
    {
        return await GetQueryable(specification).CountAsync(cancellationToken);
    }

    public async Task<IEnumerable<Query.Test>> SearchAsync(Query.Test.Specification specification, CancellationToken cancellationToken = default)
    {
        var queryable = GetQueryable(specification).OrderBy(e => e.Name);

        if (specification.MaximumRows > 0)
        {
            queryable = (IOrderedQueryable<Models.Test>)queryable.Take(specification.MaximumRows);
        }

        return (await queryable.ToListAsync(cancellationToken)).Select(e => new Query.Test
        {
            Id = e.Id,
            Name = e.Name,
            FormulaId = e.FormulaId,
            ExpectedResult = e.ExpectedResult,
            ExpectedResultDataTypeName = e.ExpectedResultDataTypeName,
            Comparison = e.Comparison
        });
    }

    public async Task<IEnumerable<Query.Test.Argument>> ArgumentsAsync(Guid testId, CancellationToken cancellationToken = default)
    {
        return (await _dbContext.TestArguments.AsNoTracking()
                .Where(e => e.TestId == testId)
                .ToListAsync(cancellationToken))
            .Select(e => new Query.Test.Argument
            {
                TestId = e.TestId,
                ArgumentId = e.ArgumentId,
                Value = e.Value
            });
    }

    private IQueryable<Models.Test> GetQueryable(Query.Test.Specification specification)
    {
        var queryable = _dbContext.Tests.AsNoTracking().AsQueryable();

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
