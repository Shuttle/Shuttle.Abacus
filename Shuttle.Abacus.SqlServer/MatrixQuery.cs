using Microsoft.EntityFrameworkCore;
using Shuttle.Contract;

namespace Shuttle.Abacus.SqlServer;

public class MatrixQuery(AbacusDbContext dbContext) : IMatrixQuery
{
    private readonly AbacusDbContext _dbContext = Guard.AgainstNull(dbContext);

    public async ValueTask<int> CountAsync(Query.Matrix.Specification specification, CancellationToken cancellationToken = default)
    {
        return await GetQueryable(specification).CountAsync(cancellationToken);
    }

    public async Task<IEnumerable<Query.Matrix>> SearchAsync(Query.Matrix.Specification specification, CancellationToken cancellationToken = default)
    {
        var queryable = GetQueryable(specification).OrderBy(e => e.Name);

        if (specification.MaximumRows > 0)
        {
            queryable = (IOrderedQueryable<Models.Matrix>)queryable.Take(specification.MaximumRows);
        }

        return (await queryable.ToListAsync(cancellationToken)).Select(e => new Query.Matrix
        {
            Id = e.Id,
            Name = e.Name,
            RowArgumentId = e.RowArgumentId,
            ColumnArgumentId = e.ColumnArgumentId,
            DataTypeName = e.DataTypeName
        });
    }

    public async Task<IEnumerable<Query.Matrix.Constraint>> ConstraintsAsync(Guid matrixId, CancellationToken cancellationToken = default)
    {
        return (await _dbContext.MatrixConstraints.AsNoTracking()
                .Where(e => e.MatrixId == matrixId)
                .ToListAsync(cancellationToken))
            .Select(e => new Query.Matrix.Constraint
            {
                Id = e.Id,
                MatrixId = e.MatrixId,
                Axis = e.Axis,
                Index = e.Index,
                Comparison = e.Comparison,
                Value = e.Value
            });
    }

    public async Task<IEnumerable<Query.Matrix.Element>> ElementsAsync(Guid matrixId, CancellationToken cancellationToken = default)
    {
        return (await _dbContext.MatrixElements.AsNoTracking()
                .Where(e => e.MatrixId == matrixId)
                .ToListAsync(cancellationToken))
            .Select(e => new Query.Matrix.Element
            {
                Id = e.Id,
                MatrixId = e.MatrixId,
                Row = e.Row,
                Column = e.Column,
                Value = e.Value
            });
    }

    private IQueryable<Models.Matrix> GetQueryable(Query.Matrix.Specification specification)
    {
        var queryable = _dbContext.Matrices.AsNoTracking().AsQueryable();

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
