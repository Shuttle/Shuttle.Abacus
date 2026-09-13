using Shuttle.Contract;
using Shuttle.Mediator;
using Shuttle.Recall;
using Shuttle.Recall.SqlServer.Storage;

namespace Shuttle.Abacus.Application;

public class RenameAlgorithm(Guid algorithmId, string name)
{
    public Guid AlgorithmId { get; } = Guard.AgainstEmpty(algorithmId);
    public string Name { get; } = Guard.AgainstEmpty(name);
}

public class RenameAlgorithmParticipant(IEventStore eventStore, IIdKeyRepository idKeyRepository) : IParticipant<RenameAlgorithm>
{
    public async Task HandleAsync(RenameAlgorithm message, CancellationToken cancellationToken = default)
    {
        Guard.AgainstNull(message);

        var algorithm = new Algorithm();
        var stream = await eventStore.GetAsync(message.AlgorithmId, cancellationToken);

        if (stream.IsEmpty)
        {
            return;
        }

        stream.Apply(algorithm);
        algorithm.Id = message.AlgorithmId;

        if (algorithm.IsNamed(message.Name))
        {
            return;
        }

        var key = Algorithm.Key(message.Name);

        if (await idKeyRepository.ContainsAsync(key, cancellationToken))
        {
            return;
        }

        await idKeyRepository.RekeyAsync(Algorithm.Key(algorithm.Name), key, cancellationToken);

        stream.Add(algorithm.Rename(message.Name));

        await eventStore.SaveAsync(stream, cancellationToken);
    }
}
