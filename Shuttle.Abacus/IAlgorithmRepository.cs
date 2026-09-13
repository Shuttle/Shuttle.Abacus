namespace Shuttle.Abacus;

public interface IAlgorithmRepository
{
    Task<IEnumerable<Algorithm>> AllAsync(CancellationToken cancellationToken = default);
}
