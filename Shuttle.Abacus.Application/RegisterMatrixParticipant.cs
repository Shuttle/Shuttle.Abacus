using Shuttle.Contract;
using Shuttle.Mediator;
using Shuttle.Recall;
using Shuttle.Recall.SqlServer.Storage;

namespace Shuttle.Abacus.Application;

public class RegisterMatrix(Guid id, string name, Guid rowArgumentId, Guid? columnArgumentId, string dataTypeName)
{
    public Guid Id { get; } = id;
    public string Name { get; } = Guard.AgainstEmpty(name);
    public Guid RowArgumentId { get; } = rowArgumentId;
    public Guid? ColumnArgumentId { get; } = columnArgumentId;
    public string DataTypeName { get; } = Guard.AgainstEmpty(dataTypeName);
}

public class RegisterMatrixParticipant(IEventStore eventStore, IIdKeyRepository idKeyRepository) : IParticipant<RegisterMatrix>
{
    public async Task HandleAsync(RegisterMatrix message, CancellationToken cancellationToken = default)
    {
        Guard.AgainstNull(message);

        var id = message.Id.Equals(Guid.Empty) ? Guid.NewGuid() : message.Id;
        var stream = await eventStore.GetAsync(id, cancellationToken);
        var matrix = stream.Get<Matrix>();

        matrix.Id = id;

        var key = Matrix.Key(message.Name);

        if (stream.IsEmpty)
        {
            if (await idKeyRepository.ContainsAsync(key, cancellationToken))
            {
                return;
            }

            await idKeyRepository.AddAsync(id, key, cancellationToken);
        }
        else if (!matrix.IsNamed(message.Name))
        {
            if (await idKeyRepository.ContainsAsync(key, cancellationToken))
            {
                return;
            }

            await idKeyRepository.RekeyAsync(Matrix.Key(matrix.Name), key, cancellationToken);
        }

        stream.Add(matrix.Register(message.Name, message.RowArgumentId, message.ColumnArgumentId, message.DataTypeName));

        await eventStore.SaveAsync(stream, cancellationToken);
    }
}
