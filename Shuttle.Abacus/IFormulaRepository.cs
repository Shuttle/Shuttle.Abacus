namespace Shuttle.Abacus;

public interface IFormulaRepository
{
    Task<IEnumerable<Formula>> AllAsync(CancellationToken cancellationToken = default);
}
