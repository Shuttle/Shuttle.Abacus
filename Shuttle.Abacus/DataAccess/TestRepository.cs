using Shuttle.Contract;
using Shuttle.Recall;

namespace Shuttle.Abacus.DataAccess;

public class TestRepository(IEventStore eventStore) : ITestRepository
{
    private readonly IEventStore _eventStore = Guard.AgainstNull(eventStore);

    public async Task<Test> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var test = new Test();
        var stream = await _eventStore.GetAsync(id, cancellationToken);

        if (stream.IsEmpty)
        {
            throw RecordNotFoundException.For("Test", id);
        }

        stream.Apply(test);

        test.Id = id;

        return test;
    }
}
