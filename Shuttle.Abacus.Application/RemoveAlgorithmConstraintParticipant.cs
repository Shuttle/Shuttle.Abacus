using Shuttle.Contract;
using Shuttle.Mediator;
using Shuttle.Recall;

namespace Shuttle.Abacus.Application;

public class RemoveAlgorithmConstraint(Guid algorithmId, Guid constraintId)
{
    public Guid AlgorithmId { get; } = Guard.AgainstEmpty(algorithmId);
    public Guid ConstraintId { get; } = Guard.AgainstEmpty(constraintId);
}

public class RemoveAlgorithmConstraintParticipant(IEventStore eventStore) : IParticipant<RemoveAlgorithmConstraint>
{
    public async Task HandleAsync(RemoveAlgorithmConstraint message, CancellationToken cancellationToken = default)
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

        if (algorithm.Removed || !algorithm.ContainsConstraint(message.ConstraintId))
        {
            return;
        }

        stream.Add(algorithm.RemoveConstraint(message.ConstraintId));

        await eventStore.SaveAsync(stream, cancellationToken);
    }
}
