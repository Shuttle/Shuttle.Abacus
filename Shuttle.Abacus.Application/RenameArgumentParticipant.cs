using Shuttle.Contract;
using Shuttle.Mediator;
using Shuttle.Recall;
using Shuttle.Recall.SqlServer.Storage;

namespace Shuttle.Abacus.Application;

public class RenameArgument(Guid argumentId, string name)
{
    public Guid ArgumentId { get; } = Guard.AgainstEmpty(argumentId);
    public string Name { get; } = Guard.AgainstEmpty(name);
}

public class RenameArgumentParticipant(IEventStore eventStore, IIdKeyRepository idKeyRepository) : IParticipant<RenameArgument>
{
    public async Task HandleAsync(RenameArgument message, CancellationToken cancellationToken = default)
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

        if (argument.IsNamed(message.Name))
        {
            return;
        }

        var key = Argument.Key(message.Name);

        if (await idKeyRepository.ContainsAsync(key, cancellationToken))
        {
            return;
        }

        await idKeyRepository.RekeyAsync(Argument.Key(argument.Name), key, cancellationToken);

        stream.Add(argument.Rename(message.Name));

        await eventStore.SaveAsync(stream, cancellationToken);
    }
}
