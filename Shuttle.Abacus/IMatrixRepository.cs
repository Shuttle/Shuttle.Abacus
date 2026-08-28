namespace Shuttle.Abacus;

public interface IMatrixRepository
{
    Task<IEnumerable<Matrix>> AllAsync(CancellationToken cancellationToken = default);
}
