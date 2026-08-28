using Shuttle.Contract;
using Shuttle.Hopper;
using Shuttle.Mediator;

namespace Shuttle.Abacus.Server.v1.MessageHandlers;

public class MatrixHandler(IMediator mediator) :
    IMessageHandler<Messages.v1.RegisterMatrix>,
    IMessageHandler<Messages.v1.RegisterMatrixConstraint>,
    IMessageHandler<Messages.v1.RegisterMatrixElement>
{
    private readonly IMediator _mediator = Guard.AgainstNull(mediator);

    public async Task HandleAsync(Messages.v1.RegisterMatrix message, CancellationToken cancellationToken = default)
    {
        await _mediator.SendAsync(new Application.RegisterMatrix(message.Id, message.Name, message.RowArgumentId, message.ColumnArgumentId, message.DataTypeName), cancellationToken);
    }

    public async Task HandleAsync(Messages.v1.RegisterMatrixConstraint message, CancellationToken cancellationToken = default)
    {
        await _mediator.SendAsync(new Application.RegisterMatrixConstraint(message.Id, message.MatrixId, message.Axis, message.Index, message.Comparison, message.Value), cancellationToken);
    }

    public async Task HandleAsync(Messages.v1.RegisterMatrixElement message, CancellationToken cancellationToken = default)
    {
        await _mediator.SendAsync(new Application.RegisterMatrixElement(message.Id, message.MatrixId, message.Row, message.Column, message.Value), cancellationToken);
    }
}
