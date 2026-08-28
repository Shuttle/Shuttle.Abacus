using Moq;
using NUnit.Framework;
using Shuttle.Abacus.Application;
using Shuttle.Abacus.Events.Argument.v1;
using Shuttle.Recall.SqlServer.Storage;

namespace Shuttle.Abacus.Tests.Participants;

[TestFixture]
public class RegisterArgumentParticipantFixture
{
    [Test]
    public async Task Should_be_able_to_register_an_argument()
    {
        var eventStore = new FixtureEventStore();
        var idKeyRepository = new Mock<IIdKeyRepository>();

        idKeyRepository.Setup(m => m.ContainsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);

        var participant = new RegisterArgumentParticipant(eventStore, idKeyRepository.Object);

        var registerArgument = new RegisterArgument(Guid.NewGuid(), "argument-name", "Text");

        await participant.HandleAsync(registerArgument, CancellationToken.None);

        var @event = eventStore.FindEvent<Registered>(registerArgument.Id);

        Assert.That(@event, Is.Not.Null);
        Assert.That(@event!.Name, Is.EqualTo("argument-name"));

        idKeyRepository.Verify(m => m.AddAsync(registerArgument.Id, Argument.Key("argument-name"), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Should_not_register_a_duplicate_name()
    {
        var eventStore = new FixtureEventStore();
        var idKeyRepository = new Mock<IIdKeyRepository>();

        idKeyRepository.Setup(m => m.ContainsAsync(Argument.Key("argument-name"), It.IsAny<CancellationToken>())).ReturnsAsync(true);

        var participant = new RegisterArgumentParticipant(eventStore, idKeyRepository.Object);

        var registerArgument = new RegisterArgument(Guid.NewGuid(), "argument-name", "Text");

        await participant.HandleAsync(registerArgument, CancellationToken.None);

        Assert.That(eventStore.FindEvent<Registered>(registerArgument.Id), Is.Null);

        idKeyRepository.Verify(m => m.AddAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
