using Shuttle.Contract;
using Shuttle.Recall;

namespace Shuttle.Abacus.DataAccess;

public class FormulaRepository(IFormulaQuery query, IEventStore eventStore) : IFormulaRepository
{
    private readonly IFormulaQuery _query = Guard.AgainstNull(query);
    private readonly IEventStore _eventStore = Guard.AgainstNull(eventStore);

    public async Task<IEnumerable<Formula>> AllAsync(CancellationToken cancellationToken = default)
    {
        var result = new List<Formula>();

        foreach (var item in await _query.SearchAsync(new(), cancellationToken))
        {
            var formula = new Formula();
            var stream = await _eventStore.GetAsync(item.Id, cancellationToken);

            stream.Apply(formula);

            formula.Id = item.Id;

            result.Add(formula);
        }

        return result;
    }
}
