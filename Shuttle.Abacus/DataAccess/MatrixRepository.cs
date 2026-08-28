using Shuttle.Contract;
using Shuttle.Recall;

namespace Shuttle.Abacus.DataAccess;

public class MatrixRepository(IMatrixQuery query, IEventStore eventStore) : IMatrixRepository
{
    private readonly IMatrixQuery _query = Guard.AgainstNull(query);
    private readonly IEventStore _eventStore = Guard.AgainstNull(eventStore);

    public async Task<IEnumerable<Matrix>> AllAsync(CancellationToken cancellationToken = default)
    {
        var result = new List<Matrix>();

        foreach (var item in await _query.SearchAsync(new(), cancellationToken))
        {
            var matrix = new Matrix();
            var stream = await _eventStore.GetAsync(item.Id, cancellationToken);

            stream.Apply(matrix);

            matrix.Id = item.Id;

            result.Add(matrix);
        }

        return result;
    }
}
