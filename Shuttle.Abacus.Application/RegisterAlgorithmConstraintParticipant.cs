using Shuttle.Contract;
using Shuttle.Mediator;
using Shuttle.Recall;

namespace Shuttle.Abacus.Application;

public class RegisterAlgorithmConstraint(Guid id, Guid algorithmId, Guid argumentId, string comparison, string value)
{
    public Guid Id { get; } = id;
    public Guid AlgorithmId { get; } = Guard.AgainstEmpty(algorithmId);
    public Guid ArgumentId { get; } = Guard.AgainstEmpty(argumentId);
    public string Comparison { get; } = Guard.AgainstEmpty(comparison);
    public string Value { get; } = Guard.AgainstEmpty(value);
}

public class RegisterAlgorithmConstraintParticipant(IEventStore eventStore) : IParticipant<RegisterAlgorithmConstraint>
{
    public async Task HandleAsync(RegisterAlgorithmConstraint message, CancellationToken cancellationToken = default)
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

        var id = message.Id.Equals(Guid.Empty) ? Guid.NewGuid() : message.Id;

        stream.Add(algorithm.RegisterConstraint(id, message.ArgumentId, message.Comparison, message.Value));

        await eventStore.SaveAsync(stream, cancellationToken);
    }
}
