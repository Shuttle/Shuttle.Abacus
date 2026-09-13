using Shuttle.Contract;
using Shuttle.Hopper;
using Shuttle.Mediator;

namespace Shuttle.Abacus.Server.v1.MessageHandlers;

public class AlgorithmHandler(IMediator mediator) :
    IMessageHandler<Messages.v1.RegisterAlgorithm>,
    IMessageHandler<Messages.v1.RenameAlgorithm>,
    IMessageHandler<Messages.v1.RemoveAlgorithm>,
    IMessageHandler<Messages.v1.RegisterAlgorithmOperation>,
    IMessageHandler<Messages.v1.RemoveAlgorithmOperation>,
    IMessageHandler<Messages.v1.RegisterAlgorithmConstraint>,
    IMessageHandler<Messages.v1.RemoveAlgorithmConstraint>
{
    private readonly IMediator _mediator = Guard.AgainstNull(mediator);

    public async Task HandleAsync(Messages.v1.RegisterAlgorithm message, CancellationToken cancellationToken = default)
    {
        await _mediator.SendAsync(new Application.RegisterAlgorithm(message.Id, message.Name), cancellationToken);
    }

    public async Task HandleAsync(Messages.v1.RenameAlgorithm message, CancellationToken cancellationToken = default)
    {
        await _mediator.SendAsync(new Application.RenameAlgorithm(message.AlgorithmId, message.Name), cancellationToken);
    }

    public async Task HandleAsync(Messages.v1.RemoveAlgorithm message, CancellationToken cancellationToken = default)
    {
        await _mediator.SendAsync(new Application.RemoveAlgorithm(message.AlgorithmId), cancellationToken);
    }

    public async Task HandleAsync(Messages.v1.RegisterAlgorithmOperation message, CancellationToken cancellationToken = default)
    {
        await _mediator.SendAsync(new Application.RegisterAlgorithmOperation(message.Id, message.AlgorithmId, message.Operation, message.ValueProviderName, message.InputParameter), cancellationToken);
    }

    public async Task HandleAsync(Messages.v1.RemoveAlgorithmOperation message, CancellationToken cancellationToken = default)
    {
        await _mediator.SendAsync(new Application.RemoveAlgorithmOperation(message.AlgorithmId, message.OperationId), cancellationToken);
    }

    public async Task HandleAsync(Messages.v1.RegisterAlgorithmConstraint message, CancellationToken cancellationToken = default)
    {
        await _mediator.SendAsync(new Application.RegisterAlgorithmConstraint(message.Id, message.AlgorithmId, message.ArgumentId, message.Comparison, message.Value), cancellationToken);
    }

    public async Task HandleAsync(Messages.v1.RemoveAlgorithmConstraint message, CancellationToken cancellationToken = default)
    {
        await _mediator.SendAsync(new Application.RemoveAlgorithmConstraint(message.AlgorithmId, message.ConstraintId), cancellationToken);
    }
}
