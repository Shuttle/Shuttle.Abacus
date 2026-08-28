using Microsoft.EntityFrameworkCore;
using Shuttle.Contract;

namespace Shuttle.Abacus.SqlServer;

public class ArgumentQuery(AbacusDbContext dbContext) : IArgumentQuery
{
    private readonly AbacusDbContext _dbContext = Guard.AgainstNull(dbContext);

    public async ValueTask<int> CountAsync(Query.Argument.Specification specification, CancellationToken cancellationToken = default)
    {
        return await GetQueryable(specification).CountAsync(cancellationToken);
    }

    public async Task<IEnumerable<Query.Argument>> SearchAsync(Query.Argument.Specification specification, CancellationToken cancellationToken = default)
    {
        var queryable = GetQueryable(specification).OrderBy(e => e.Name);

        if (specification.MaximumRows > 0)
        {
            queryable = (IOrderedQueryable<Models.Argument>)queryable.Take(specification.MaximumRows);
        }

        return (await queryable.ToListAsync(cancellationToken)).Select(e => new Query.Argument
        {
            Id = e.Id,
            Name = e.Name,
            DataTypeName = e.DataTypeName
        });
    }

    public async Task<IEnumerable<Query.Argument.Value>> ValuesAsync(Guid argumentId, CancellationToken cancellationToken = default)
    {
        return (await _dbContext.ArgumentValues.AsNoTracking()
                .Where(e => e.ArgumentId == argumentId)
                .OrderBy(e => e.Value)
                .ToListAsync(cancellationToken))
            .Select(e => new Query.Argument.Value
            {
                ArgumentId = e.ArgumentId,
                ArgumentValue = e.Value
            });
    }

    private IQueryable<Models.Argument> GetQueryable(Query.Argument.Specification specification)
    {
        var queryable = _dbContext.Arguments.AsNoTracking().AsQueryable();

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
