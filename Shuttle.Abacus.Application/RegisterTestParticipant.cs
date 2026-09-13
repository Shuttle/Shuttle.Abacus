using Shuttle.Contract;
using Shuttle.Mediator;
using Shuttle.Recall;
using Shuttle.Recall.SqlServer.Storage;

namespace Shuttle.Abacus.Application;

public class RegisterTest(Guid id, string name, Guid algorithmId, string expectedResult, string expectedResultDataTypeName, string comparison)
{
    public Guid Id { get; } = Guard.AgainstEmpty(id);
    public string Name { get; } = Guard.AgainstEmpty(name);
    public Guid AlgorithmId { get; } = Guard.AgainstEmpty(algorithmId);
    public string ExpectedResult { get; } = Guard.AgainstEmpty(expectedResult);
    public string ExpectedResultDataTypeName { get; } = Guard.AgainstEmpty(expectedResultDataTypeName);
    public string Comparison { get; } = Guard.AgainstEmpty(comparison);
}

public class RegisterTestParticipant(IEventStore eventStore, IIdKeyRepository idKeyRepository) : IParticipant<RegisterTest>
{
    public async Task HandleAsync(RegisterTest message, CancellationToken cancellationToken = default)
    {
        Guard.AgainstNull(message);

        var key = Test.Key(message.Name);

        if (await idKeyRepository.ContainsAsync(key, cancellationToken))
        {
            return;
        }

        await idKeyRepository.AddAsync(message.Id, key, cancellationToken);

        var stream = (await eventStore.GetAsync(message.Id, cancellationToken)).MustBeEmpty();
        var aggregate = stream.Get<Test>();

        stream.Add(aggregate.Register(message.Name, message.AlgorithmId, message.ExpectedResult, message.ExpectedResultDataTypeName, message.Comparison));

        await eventStore.SaveAsync(stream, cancellationToken);
    }
}
