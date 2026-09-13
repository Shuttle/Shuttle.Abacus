using Shuttle.Contract;
using Shuttle.Mediator;
using Shuttle.Recall;

namespace Shuttle.Abacus.Application;

public class RemoveAlgorithmOperation(Guid algorithmId, Guid operationId)
{
    public Guid AlgorithmId { get; } = Guard.AgainstEmpty(algorithmId);
    public Guid OperationId { get; } = Guard.AgainstEmpty(operationId);
}

public class RemoveAlgorithmOperationParticipant(IEventStore eventStore) : IParticipant<RemoveAlgorithmOperation>
{
    public async Task HandleAsync(RemoveAlgorithmOperation message, CancellationToken cancellationToken = default)
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

        if (algorithm.Removed || !algorithm.ContainsOperation(message.OperationId))
        {
            return;
        }

        stream.Add(algorithm.RemoveOperation(message.OperationId));

        await eventStore.SaveAsync(stream, cancellationToken);
    }
}
