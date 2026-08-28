using Shuttle.Contract;
using Shuttle.Mediator;
using Shuttle.Recall;

namespace Shuttle.Abacus.Application;

public class RegisterMatrixConstraint(Guid id, Guid matrixId, string axis, int index, string comparison, string value)
{
    public Guid Id { get; } = id;
    public Guid MatrixId { get; } = Guard.AgainstEmpty(matrixId);
    public string Axis { get; } = Guard.AgainstEmpty(axis);
    public int Index { get; } = index;
    public string Comparison { get; } = Guard.AgainstEmpty(comparison);
    public string Value { get; } = Guard.AgainstEmpty(value);
}

public class RegisterMatrixConstraintParticipant(IEventStore eventStore) : IParticipant<RegisterMatrixConstraint>
{
    public async Task HandleAsync(RegisterMatrixConstraint message, CancellationToken cancellationToken = default)
    {
        Guard.AgainstNull(message);

        var matrix = new Matrix();
        var stream = await eventStore.GetAsync(message.MatrixId, cancellationToken);

        if (stream.IsEmpty)
        {
            return;
        }

        stream.Apply(matrix);
        matrix.Id = message.MatrixId;

        if (matrix.Removed)
        {
            return;
        }

        var id = message.Id.Equals(Guid.Empty) ? Guid.NewGuid() : message.Id;

        stream.Add(matrix.RegisterConstraint(id, message.Axis, message.Index, message.Comparison, message.Value));

        await eventStore.SaveAsync(stream, cancellationToken);
    }
}
