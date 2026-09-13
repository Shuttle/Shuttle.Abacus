using Shuttle.Contract;
using Shuttle.Mediator;
using Shuttle.Recall;
using Shuttle.Recall.SqlServer.Storage;

namespace Shuttle.Abacus.Application;

public class RegisterAlgorithm(Guid id, string name)
{
    public Guid Id { get; } = Guard.AgainstEmpty(id);
    public string Name { get; } = Guard.AgainstEmpty(name);
}

public class RegisterAlgorithmParticipant(IEventStore eventStore, IIdKeyRepository idKeyRepository) : IParticipant<RegisterAlgorithm>
{
    public async Task HandleAsync(RegisterAlgorithm message, CancellationToken cancellationToken = default)
    {
        Guard.AgainstNull(message);

        var key = Algorithm.Key(message.Name);

        if (await idKeyRepository.ContainsAsync(key, cancellationToken))
        {
            return;
        }

        await idKeyRepository.AddAsync(message.Id, key, cancellationToken);

        var stream = (await eventStore.GetAsync(message.Id, cancellationToken)).MustBeEmpty();
        var aggregate = stream.Get<Algorithm>();

        stream.Add(aggregate.Register(message.Name));

        await eventStore.SaveAsync(stream, cancellationToken);
    }
}
