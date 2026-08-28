using Shuttle.Contract;
using Shuttle.Mediator;
using Shuttle.Recall;

namespace Shuttle.Abacus.Application;

public class RemoveTestArgument(Guid testId, Guid argumentId)
{
    public Guid TestId { get; } = Guard.AgainstEmpty(testId);
    public Guid ArgumentId { get; } = Guard.AgainstEmpty(argumentId);
}

public class RemoveTestArgumentParticipant(IEventStore eventStore) : IParticipant<RemoveTestArgument>
{
    public async Task HandleAsync(RemoveTestArgument message, CancellationToken cancellationToken = default)
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

        stream.Add(test.RemoveArgument(message.ArgumentId));

        await eventStore.SaveAsync(stream, cancellationToken);
    }
}
