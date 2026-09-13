using Shuttle.Contract;
using Shuttle.Hopper;
using Shuttle.Mediator;

namespace Shuttle.Abacus.Server.v1.MessageHandlers;

public class TestHandler(IMediator mediator) :
    IMessageHandler<Messages.v1.RegisterTest>,
    IMessageHandler<Messages.v1.RemoveTest>,
    IMessageHandler<Messages.v1.RegisterTestArgument>,
    IMessageHandler<Messages.v1.RemoveTestArgument>
{
    private readonly IMediator _mediator = Guard.AgainstNull(mediator);

    public async Task HandleAsync(Messages.v1.RegisterTest message, CancellationToken cancellationToken = default)
    {
        await _mediator.SendAsync(new Application.RegisterTest(message.Id, message.Name, message.AlgorithmId, message.ExpectedResult, message.ExpectedResultDataTypeName, message.Comparison), cancellationToken);
    }

    public async Task HandleAsync(Messages.v1.RemoveTest message, CancellationToken cancellationToken = default)
    {
        await _mediator.SendAsync(new Application.RemoveTest(message.TestId), cancellationToken);
    }

    public async Task HandleAsync(Messages.v1.RegisterTestArgument message, CancellationToken cancellationToken = default)
    {
        await _mediator.SendAsync(new Application.RegisterTestArgument(message.TestId, message.ArgumentId, message.Value), cancellationToken);
    }

    public async Task HandleAsync(Messages.v1.RemoveTestArgument message, CancellationToken cancellationToken = default)
    {
        await _mediator.SendAsync(new Application.RemoveTestArgument(message.TestId, message.ArgumentId), cancellationToken);
    }
}
