using Shuttle.Abacus.Events.Matrix.v1;
using Shuttle.Abacus.SqlServer;
using Shuttle.Contract;
using Shuttle.Hopper;
using Shuttle.Recall;

namespace Shuttle.Abacus.EventProcessing.v1.EventHandlers;

public class MatrixHandler(AbacusDbContext dbContext, IBus bus) :
    IEventHandler<Registered>,
    IEventHandler<ConstraintRegistered>,
    IEventHandler<ElementRegistered>
{
    private readonly AbacusDbContext _dbContext = Guard.AgainstNull(dbContext);
    private readonly IBus _bus = Guard.AgainstNull(bus);

    public async Task HandleAsync(IEventHandlerContext<Registered> context, CancellationToken cancellationToken = default)
    {
        var matrix = await _dbContext.Matrices.FindAsync([context.PrimitiveEvent.Id], cancellationToken);

        if (matrix == null)
        {
            matrix = new()
            {
                Id = context.PrimitiveEvent.Id
            };

            _dbContext.Matrices.Add(matrix);
        }
        else
        {
            // `Register` re-registration (matrix edit) clears constraints/elements to match the aggregate.
            _dbContext.MatrixConstraints.RemoveRange(_dbContext.MatrixConstraints.Where(e => e.MatrixId == matrix.Id));
            _dbContext.MatrixElements.RemoveRange(_dbContext.MatrixElements.Where(e => e.MatrixId == matrix.Id));
        }

        matrix.Name = context.Event.Name;
        matrix.RowArgumentId = context.Event.RowArgumentId;
        matrix.ColumnArgumentId = context.Event.ColumnArgumentId;
        matrix.DataTypeName = context.Event.DataTypeName;

        await _dbContext.SaveChangesAsync(cancellationToken);
        await _bus.PublishAsync(new Messages.v1.MatrixRegistered
        {
            Id = context.PrimitiveEvent.Id,
            Name = context.Event.Name,
            RowArgumentId = context.Event.RowArgumentId,
            ColumnArgumentId = context.Event.ColumnArgumentId,
            DataTypeName = context.Event.DataTypeName
        }, cancellationToken);
    }

    public async Task HandleAsync(IEventHandlerContext<ConstraintRegistered> context, CancellationToken cancellationToken = default)
    {
        var constraint = await _dbContext.MatrixConstraints.FindAsync([context.PrimitiveEvent.Id, context.Event.Axis, context.Event.Index], cancellationToken);

        if (constraint == null)
        {
            _dbContext.MatrixConstraints.Add(new()
            {
                Id = context.Event.Id,
                MatrixId = context.PrimitiveEvent.Id,
                Axis = context.Event.Axis,
                Index = context.Event.Index,
                Comparison = context.Event.Comparison,
                Value = context.Event.Value
            });
        }
        else
        {
            constraint.Id = context.Event.Id;
            constraint.Comparison = context.Event.Comparison;
            constraint.Value = context.Event.Value;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        await _bus.PublishAsync(new Messages.v1.MatrixConstraintRegistered
        {
            Id = context.Event.Id,
            MatrixId = context.PrimitiveEvent.Id,
            Axis = context.Event.Axis,
            Index = context.Event.Index,
            Comparison = context.Event.Comparison,
            Value = context.Event.Value
        }, cancellationToken);
    }

    public async Task HandleAsync(IEventHandlerContext<ElementRegistered> context, CancellationToken cancellationToken = default)
    {
        var element = await _dbContext.MatrixElements.FindAsync([context.PrimitiveEvent.Id, context.Event.Row, context.Event.Column], cancellationToken);

        if (element == null)
        {
            _dbContext.MatrixElements.Add(new()
            {
                Id = context.Event.Id,
                MatrixId = context.PrimitiveEvent.Id,
                Row = context.Event.Row,
                Column = context.Event.Column,
                Value = context.Event.Value
            });
        }
        else
        {
            element.Id = context.Event.Id;
            element.Value = context.Event.Value;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        await _bus.PublishAsync(new Messages.v1.MatrixElementRegistered
        {
            Id = context.Event.Id,
            MatrixId = context.PrimitiveEvent.Id,
            Row = context.Event.Row,
            Column = context.Event.Column,
            Value = context.Event.Value
        }, cancellationToken);
    }
}
