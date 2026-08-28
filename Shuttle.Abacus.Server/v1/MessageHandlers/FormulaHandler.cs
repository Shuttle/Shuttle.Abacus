using Shuttle.Contract;
using Shuttle.Hopper;
using Shuttle.Mediator;

namespace Shuttle.Abacus.Server.v1.MessageHandlers;

public class FormulaHandler(IMediator mediator) :
    IMessageHandler<Messages.v1.RegisterFormula>,
    IMessageHandler<Messages.v1.RenameFormula>,
    IMessageHandler<Messages.v1.RemoveFormula>,
    IMessageHandler<Messages.v1.RegisterFormulaOperation>,
    IMessageHandler<Messages.v1.RemoveFormulaOperation>,
    IMessageHandler<Messages.v1.RegisterFormulaConstraint>,
    IMessageHandler<Messages.v1.RemoveFormulaConstraint>
{
    private readonly IMediator _mediator = Guard.AgainstNull(mediator);

    public async Task HandleAsync(Messages.v1.RegisterFormula message, CancellationToken cancellationToken = default)
    {
        await _mediator.SendAsync(new Application.RegisterFormula(message.Id, message.Name), cancellationToken);
    }

    public async Task HandleAsync(Messages.v1.RenameFormula message, CancellationToken cancellationToken = default)
    {
        await _mediator.SendAsync(new Application.RenameFormula(message.FormulaId, message.Name), cancellationToken);
    }

    public async Task HandleAsync(Messages.v1.RemoveFormula message, CancellationToken cancellationToken = default)
    {
        await _mediator.SendAsync(new Application.RemoveFormula(message.FormulaId), cancellationToken);
    }

    public async Task HandleAsync(Messages.v1.RegisterFormulaOperation message, CancellationToken cancellationToken = default)
    {
        await _mediator.SendAsync(new Application.RegisterFormulaOperation(message.Id, message.FormulaId, message.Operation, message.ValueProviderName, message.InputParameter), cancellationToken);
    }

    public async Task HandleAsync(Messages.v1.RemoveFormulaOperation message, CancellationToken cancellationToken = default)
    {
        await _mediator.SendAsync(new Application.RemoveFormulaOperation(message.FormulaId, message.OperationId), cancellationToken);
    }

    public async Task HandleAsync(Messages.v1.RegisterFormulaConstraint message, CancellationToken cancellationToken = default)
    {
        await _mediator.SendAsync(new Application.RegisterFormulaConstraint(message.Id, message.FormulaId, message.ArgumentId, message.Comparison, message.Value), cancellationToken);
    }

    public async Task HandleAsync(Messages.v1.RemoveFormulaConstraint message, CancellationToken cancellationToken = default)
    {
        await _mediator.SendAsync(new Application.RemoveFormulaConstraint(message.FormulaId, message.ConstraintId), cancellationToken);
    }
}
