using Shuttle.Contract;
using Shuttle.Mediator;
using Shuttle.Recall;
using Shuttle.Recall.SqlServer.Storage;

namespace Shuttle.Abacus.Application;

public class RegisterArgument(Guid id, string name, string dataTypeName)
{
    public Guid Id { get; } = Guard.AgainstEmpty(id);
    public string Name { get; } = Guard.AgainstEmpty(name);
    public string DataTypeName { get; } = Guard.AgainstEmpty(dataTypeName);
}

public class RegisterArgumentParticipant(IEventStore eventStore, IIdKeyRepository idKeyRepository) : IParticipant<RegisterArgument>
{
    public async Task HandleAsync(RegisterArgument message, CancellationToken cancellationToken = default)
    {
        Guard.AgainstNull(message);

        var key = Argument.Key(message.Name);

        if (await idKeyRepository.ContainsAsync(key, cancellationToken))
        {
            return;
        }

        await idKeyRepository.AddAsync(message.Id, key, cancellationToken);

        var stream = (await eventStore.GetAsync(message.Id, cancellationToken)).MustBeEmpty();
        var aggregate = stream.Get<Argument>();

        stream.Add(aggregate.Register(message.Name, message.DataTypeName));

        await eventStore.SaveAsync(stream, cancellationToken);
    }
}
