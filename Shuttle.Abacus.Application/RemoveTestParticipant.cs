using Shuttle.Contract;
using Shuttle.Mediator;
using Shuttle.Recall;
using Shuttle.Recall.SqlServer.Storage;

namespace Shuttle.Abacus.Application;

public class RemoveTest(Guid testId)
{
    public Guid TestId { get; } = Guard.AgainstEmpty(testId);
}

public class RemoveTestParticipant(IEventStore eventStore, IIdKeyRepository idKeyRepository) : IParticipant<RemoveTest>
{
    public async Task HandleAsync(RemoveTest message, CancellationToken cancellationToken = default)
    {
        Guard.AgainstNull(message);

        var test = new Test();
        var stream = await eventStore.GetAsync(message.TestId, cancellationToken);

        if (stream.IsEmpty)
        {
            return;
        }

        stream.Apply(test);
        test.Id = message.TestId;

        if (test.Removed)
        {
            return;
        }

        stream.Add(test.Remove());

        await idKeyRepository.RemoveAsync(message.TestId, cancellationToken);

        await eventStore.SaveAsync(stream, cancellationToken);
    }
}
