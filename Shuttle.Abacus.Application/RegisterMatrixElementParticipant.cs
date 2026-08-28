using Shuttle.Contract;
using Shuttle.Mediator;
using Shuttle.Recall;

namespace Shuttle.Abacus.Application;

public class RegisterMatrixElement(Guid id, Guid matrixId, int row, int column, string value)
{
    public Guid Id { get; } = id;
    public Guid MatrixId { get; } = Guard.AgainstEmpty(matrixId);
    public int Row { get; } = row;
    public int Column { get; } = column;
    public string Value { get; } = Guard.AgainstEmpty(value);
}

public class RegisterMatrixElementParticipant(IEventStore eventStore) : IParticipant<RegisterMatrixElement>
{
    public async Task HandleAsync(RegisterMatrixElement message, CancellationToken cancellationToken = default)
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

        stream.Add(matrix.RegisterElement(id, message.Row, message.Column, message.Value));

        await eventStore.SaveAsync(stream, cancellationToken);
    }
}
