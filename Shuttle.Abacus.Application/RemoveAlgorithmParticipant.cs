using Shuttle.Contract;
using Shuttle.Mediator;
using Shuttle.Recall;
using Shuttle.Recall.SqlServer.Storage;

namespace Shuttle.Abacus.Application;

public class RemoveAlgorithm(Guid algorithmId)
{
    public Guid AlgorithmId { get; } = Guard.AgainstEmpty(algorithmId);
}

public class RemoveAlgorithmParticipant(IEventStore eventStore, IIdKeyRepository idKeyRepository) : IParticipant<RemoveAlgorithm>
{
    public async Task HandleAsync(RemoveAlgorithm message, CancellationToken cancellationToken = default)
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

        if (algorithm.Removed)
        {
            return;
        }

        stream.Add(algorithm.Remove());

        await idKeyRepository.RemoveAsync(message.AlgorithmId, cancellationToken);

        await eventStore.SaveAsync(stream, cancellationToken);
    }
}
