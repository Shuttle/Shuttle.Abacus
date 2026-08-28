using Shuttle.Contract;
using Shuttle.Mediator;
using Shuttle.Recall;

namespace Shuttle.Abacus.Application;

public class RegisterTestArgument(Guid testId, Guid argumentId, string value)
{
    public Guid TestId { get; } = Guard.AgainstEmpty(testId);
    public Guid ArgumentId { get; } = Guard.AgainstEmpty(argumentId);
    public string Value { get; } = Guard.AgainstEmpty(value);
}

public class RegisterTestArgumentParticipant(IEventStore eventStore) : IParticipant<RegisterTestArgument>
{
    public async Task HandleAsync(RegisterTestArgument message, CancellationToken cancellationToken = default)
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

        stream.Add(test.RegisterArgument(message.ArgumentId, message.Value));

        await eventStore.SaveAsync(stream, cancellationToken);
    }
}
