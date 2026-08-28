using Shuttle.Contract;
using Shuttle.Hopper;
using Shuttle.Mediator;

namespace Shuttle.Abacus.Server.v1.MessageHandlers;

public class ArgumentHandler(IMediator mediator) :
    IMessageHandler<Messages.v1.RegisterArgument>,
    IMessageHandler<Messages.v1.RenameArgument>,
    IMessageHandler<Messages.v1.RemoveArgument>,
    IMessageHandler<Messages.v1.RegisterArgumentValue>,
    IMessageHandler<Messages.v1.RemoveArgumentValue>
{
    private readonly IMediator _mediator = Guard.AgainstNull(mediator);

    public async Task HandleAsync(Messages.v1.RegisterArgument message, CancellationToken cancellationToken = default)
    {
        await _mediator.SendAsync(new Application.RegisterArgument(message.Id, message.Name, message.DataTypeName), cancellationToken);
    }

    public async Task HandleAsync(Messages.v1.RenameArgument message, CancellationToken cancellationToken = default)
    {
        await _mediator.SendAsync(new Application.RenameArgument(message.ArgumentId, message.Name), cancellationToken);
    }

    public async Task HandleAsync(Messages.v1.RemoveArgument message, CancellationToken cancellationToken = default)
    {
        await _mediator.SendAsync(new Application.RemoveArgument(message.ArgumentId), cancellationToken);
    }

    public async Task HandleAsync(Messages.v1.RegisterArgumentValue message, CancellationToken cancellationToken = default)
    {
        await _mediator.SendAsync(new Application.RegisterArgumentValue(message.ArgumentId, message.Value), cancellationToken);
    }

    public async Task HandleAsync(Messages.v1.RemoveArgumentValue message, CancellationToken cancellationToken = default)
    {
        await _mediator.SendAsync(new Application.RemoveArgumentValue(message.ArgumentId, message.Value), cancellationToken);
    }
}
