using Shuttle.Contract;
using Shuttle.Recall;

namespace Shuttle.Abacus.DataAccess;

public class AlgorithmRepository(IAlgorithmQuery query, IEventStore eventStore) : IAlgorithmRepository
{
    private readonly IAlgorithmQuery _query = Guard.AgainstNull(query);
    private readonly IEventStore _eventStore = Guard.AgainstNull(eventStore);

    public async Task<IEnumerable<Algorithm>> AllAsync(CancellationToken cancellationToken = default)
    {
        var result = new List<Algorithm>();

        foreach (var item in await _query.SearchAsync(new(), cancellationToken))
        {
            var algorithm = new Algorithm();
            var stream = await _eventStore.GetAsync(item.Id, cancellationToken);

            stream.Apply(algorithm);

            algorithm.Id = item.Id;

            result.Add(algorithm);
        }

        return result;
    }
}
