using Microsoft.EntityFrameworkCore;
using Shuttle.Contract;

namespace Shuttle.Abacus.SqlServer;

public class FormulaQuery(AbacusDbContext dbContext) : IFormulaQuery
{
    private readonly AbacusDbContext _dbContext = Guard.AgainstNull(dbContext);

    public async ValueTask<int> CountAsync(Query.Formula.Specification specification, CancellationToken cancellationToken = default)
    {
        return await GetQueryable(specification).CountAsync(cancellationToken);
    }

    public async Task<IEnumerable<Query.Formula>> SearchAsync(Query.Formula.Specification specification, CancellationToken cancellationToken = default)
    {
        var queryable = GetQueryable(specification).OrderBy(e => e.Name);

        if (specification.MaximumRows > 0)
        {
            queryable = (IOrderedQueryable<Models.Formula>)queryable.Take(specification.MaximumRows);
        }

        return (await queryable.ToListAsync(cancellationToken)).Select(e => new Query.Formula
        {
            Id = e.Id,
            Name = e.Name,
            MaximumFormulaName = e.MaximumFormulaName,
            MinimumFormulaName = e.MinimumFormulaName
        });
    }

    public async Task<IEnumerable<Query.Formula.Operation>> OperationsAsync(Guid formulaId, CancellationToken cancellationToken = default)
    {
        return (await _dbContext.FormulaOperations.AsNoTracking()
                .Where(e => e.FormulaId == formulaId)
                .OrderBy(e => e.SequenceNumber)
                .ToListAsync(cancellationToken))
            .Select(e => new Query.Formula.Operation
            {
                Id = e.Id,
                FormulaId = e.FormulaId,
                SequenceNumber = e.SequenceNumber,
                OperationName = e.Operation,
                ValueProviderName = e.ValueProviderName,
                InputParameter = e.InputParameter
            });
    }

    public async Task<IEnumerable<Query.Formula.Constraint>> ConstraintsAsync(Guid formulaId, CancellationToken cancellationToken = default)
    {
        return (await _dbContext.FormulaConstraints.AsNoTracking()
                .Where(e => e.FormulaId == formulaId)
                .ToListAsync(cancellationToken))
            .Select(e => new Query.Formula.Constraint
            {
                Id = e.Id,
                FormulaId = e.FormulaId,
                ArgumentId = e.ArgumentId,
                Comparison = e.Comparison,
                Value = e.Value
            });
    }

    private IQueryable<Models.Formula> GetQueryable(Query.Formula.Specification specification)
    {
        var queryable = _dbContext.Formulas.AsNoTracking().AsQueryable();

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
