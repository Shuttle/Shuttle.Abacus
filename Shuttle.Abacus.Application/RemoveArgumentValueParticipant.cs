using Shuttle.Contract;
using Shuttle.Mediator;
using Shuttle.Recall;

namespace Shuttle.Abacus.Application;

public class RemoveArgumentValue(Guid argumentId, string value)
{
    public Guid ArgumentId { get; } = Guard.AgainstEmpty(argumentId);
    public string Value { get; } = Guard.AgainstEmpty(value);
}

public class RemoveArgumentValueParticipant(IEventStore eventStore) : IParticipant<RemoveArgumentValue>
{
    public async Task HandleAsync(RemoveArgumentValue message, CancellationToken cancellationToken = default)
    {
        Guard.AgainstNull(message);

        var argument = new Argument();
        var stream = await eventStore.GetAsync(message.ArgumentId, cancellationToken);

        if (stream.IsEmpty)
        {
            return;
        }

        stream.Apply(argument);
        argument.Id = message.ArgumentId;

        if (!argument.ContainsValue(message.Value))
        {
            return;
        }

        stream.Add(argument.RemoveValue(message.Value));

        await eventStore.SaveAsync(stream, cancellationToken);
    }
}
