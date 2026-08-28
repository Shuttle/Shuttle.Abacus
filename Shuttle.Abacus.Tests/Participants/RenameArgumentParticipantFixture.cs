using Moq;
using NUnit.Framework;
using Shuttle.Abacus.Application;
using Shuttle.Abacus.Events.Argument.v1;
using Shuttle.Recall;
using Shuttle.Recall.SqlServer.Storage;

namespace Shuttle.Abacus.Tests.Participants;

[TestFixture]
public class RenameArgumentParticipantFixture
{
    [Test]
    public async Task Should_be_able_to_rename_an_argument()
    {
        var eventStore = new FixtureEventStore();
        var idKeyRepository = new Mock<IIdKeyRepository>();

        idKeyRepository.Setup(m => m.ContainsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);

        var argumentId = Guid.NewGuid();
        var stream = await eventStore.GetAsync(argumentId);
        var argument = new Argument();

        stream.Add(argument.Register("original-name", "Text"));

        await eventStore.SaveAsync(stream);

        var participant = new RenameArgumentParticipant(eventStore, idKeyRepository.Object);

        await participant.HandleAsync(new RenameArgument(argumentId, "new-name"), CancellationToken.None);

        var @event = eventStore.FindEvent<Renamed>(argumentId);

        Assert.That(@event, Is.Not.Null);
        Assert.That(@event!.Name, Is.EqualTo("new-name"));

        idKeyRepository.Verify(m => m.RekeyAsync(Argument.Key("original-name"), Argument.Key("new-name"), It.IsAny<CancellationToken>()), Times.Once);
    }
}
