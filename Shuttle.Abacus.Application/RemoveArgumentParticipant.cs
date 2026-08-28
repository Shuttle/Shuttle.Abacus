using Shuttle.Contract;
using Shuttle.Mediator;
using Shuttle.Recall;
using Shuttle.Recall.SqlServer.Storage;

namespace Shuttle.Abacus.Application;

public class RemoveArgument(Guid argumentId)
{
    public Guid ArgumentId { get; } = Guard.AgainstEmpty(argumentId);
}

public class RemoveArgumentParticipant(IEventStore eventStore, IIdKeyRepository idKeyRepository) : IParticipant<RemoveArgument>
{
    public async Task HandleAsync(RemoveArgument message, CancellationToken cancellationToken = default)
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

        if (argument.Removed)
        {
            return;
        }

        stream.Add(argument.Remove());

        await idKeyRepository.RemoveAsync(message.ArgumentId, cancellationToken);

        await eventStore.SaveAsync(stream, cancellationToken);
    }
}
