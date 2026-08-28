namespace Shuttle.Abacus;

public interface ITestRepository
{
    Task<Test> GetAsync(Guid id, CancellationToken cancellationToken = default);
}
