using Shuttle.Contract;
using Shuttle.Recall;

namespace Shuttle.Abacus.DataAccess;

public class ArgumentRepository(IArgumentQuery query, IEventStore eventStore) : IArgumentRepository
{
    private readonly IArgumentQuery _query = Guard.AgainstNull(query);
    private readonly IEventStore _eventStore = Guard.AgainstNull(eventStore);

    public async Task<IEnumerable<Argument>> AllAsync(CancellationToken cancellationToken = default)
    {
        var result = new List<Argument>();

        foreach (var item in await _query.SearchAsync(new(), cancellationToken))
        {
            var argument = new Argument();
            var stream = await _eventStore.GetAsync(item.Id, cancellationToken);

            stream.Apply(argument);

            argument.Id = item.Id;

            result.Add(argument);
        }

        return result;
    }
}
