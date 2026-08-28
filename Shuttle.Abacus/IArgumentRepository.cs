namespace Shuttle.Abacus;

public interface IArgumentRepository
{
    Task<IEnumerable<Argument>> AllAsync(CancellationToken cancellationToken = default);
}
