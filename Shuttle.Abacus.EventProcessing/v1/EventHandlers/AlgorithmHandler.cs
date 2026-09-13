using Shuttle.Abacus.Events.Algorithm.v1;
using Shuttle.Abacus.SqlServer;
using Shuttle.Contract;
using Shuttle.Hopper;
using Shuttle.Recall;

namespace Shuttle.Abacus.EventProcessing.v1.EventHandlers;

public class AlgorithmHandler(AbacusDbContext dbContext, IBus bus) :
    IEventHandler<Registered>,
    IEventHandler<Renamed>,
    IEventHandler<Removed>,
    IEventHandler<OperationRegistered>,
    IEventHandler<OperationRemoved>,
    IEventHandler<ConstraintRegistered>,
    IEventHandler<ConstraintRemoved>
{
    private readonly AbacusDbContext _dbContext = Guard.AgainstNull(dbContext);
    private readonly IBus _bus = Guard.AgainstNull(bus);

    public async Task HandleAsync(IEventHandlerContext<Registered> context, CancellationToken cancellationToken = default)
    {
        _dbContext.Algorithms.Add(new()
        {
            Id = context.PrimitiveEvent.Id,
            Name = context.Event.Name
        });

        await _dbContext.SaveChangesAsync(cancellationToken);
        await _bus.PublishAsync(new Messages.v1.AlgorithmRegistered
        {
            Id = context.PrimitiveEvent.Id,
            Name = context.Event.Name
        }, cancellationToken);
    }

    public async Task HandleAsync(IEventHandlerContext<Renamed> context, CancellationToken cancellationToken = default)
    {
        var algorithm = await _dbContext.Algorithms.FindAsync([context.PrimitiveEvent.Id], cancellationToken);

        if (algorithm == null)
        {
            return;
        }

        algorithm.Name = context.Event.Name;

        await _dbContext.SaveChangesAsync(cancellationToken);
        await _bus.PublishAsync(new Messages.v1.AlgorithmRenamed
        {
            Id = context.PrimitiveEvent.Id,
            Name = context.Event.Name
        }, cancellationToken);
    }

    public async Task HandleAsync(IEventHandlerContext<Removed> context, CancellationToken cancellationToken = default)
    {
        var algorithm = await _dbContext.Algorithms.FindAsync([context.PrimitiveEvent.Id], cancellationToken);

        if (algorithm == null)
        {
            return;
        }

        _dbContext.Algorithms.Remove(algorithm);

        await _dbContext.SaveChangesAsync(cancellationToken);
        await _bus.PublishAsync(new Messages.v1.AlgorithmRemoved
        {
            Id = context.PrimitiveEvent.Id
        }, cancellationToken);
    }

    public async Task HandleAsync(IEventHandlerContext<OperationRegistered> context, CancellationToken cancellationToken = default)
    {
        var operation = await _dbContext.AlgorithmOperations.FindAsync([context.PrimitiveEvent.Id, context.Event.SequenceNumber], cancellationToken);

        if (operation == null)
        {
            _dbContext.AlgorithmOperations.Add(new()
            {
                Id = context.Event.Id,
                AlgorithmId = context.PrimitiveEvent.Id,
                SequenceNumber = context.Event.SequenceNumber,
                Operation = context.Event.Operation,
                ValueProviderName = context.Event.ValueProviderName,
                InputParameter = context.Event.InputParameter
            });
        }
        else
        {
            operation.Id = context.Event.Id;
            operation.Operation = context.Event.Operation;
            operation.ValueProviderName = context.Event.ValueProviderName;
            operation.InputParameter = context.Event.InputParameter;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        await _bus.PublishAsync(new Messages.v1.AlgorithmOperationRegistered
        {
            Id = context.Event.Id,
            AlgorithmId = context.PrimitiveEvent.Id,
            SequenceNumber = context.Event.SequenceNumber,
            Operation = context.Event.Operation,
            ValueProviderName = context.Event.ValueProviderName,
            InputParameter = context.Event.InputParameter
        }, cancellationToken);
    }

    public async Task HandleAsync(IEventHandlerContext<OperationRemoved> context, CancellationToken cancellationToken = default)
    {
        var operation = await _dbContext.AlgorithmOperations.FindAsync([context.PrimitiveEvent.Id, context.Event.SequenceNumber], cancellationToken);

        if (operation == null)
        {
            return;
        }

        _dbContext.AlgorithmOperations.Remove(operation);

        await _dbContext.SaveChangesAsync(cancellationToken);
        await _bus.PublishAsync(new Messages.v1.AlgorithmOperationRemoved
        {
            AlgorithmId = context.PrimitiveEvent.Id,
            Id = context.Event.Id
        }, cancellationToken);
    }

    public async Task HandleAsync(IEventHandlerContext<ConstraintRegistered> context, CancellationToken cancellationToken = default)
    {
        var constraint = await _dbContext.AlgorithmConstraints.FindAsync([context.Event.Id], cancellationToken);

        if (constraint == null)
        {
            _dbContext.AlgorithmConstraints.Add(new()
            {
                Id = context.Event.Id,
                AlgorithmId = context.PrimitiveEvent.Id,
                ArgumentId = context.Event.ArgumentId,
                Comparison = context.Event.Comparison,
                Value = context.Event.Value
            });
        }
        else
        {
            constraint.ArgumentId = context.Event.ArgumentId;
            constraint.Comparison = context.Event.Comparison;
            constraint.Value = context.Event.Value;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        await _bus.PublishAsync(new Messages.v1.AlgorithmConstraintRegistered
        {
            Id = context.Event.Id,
            AlgorithmId = context.PrimitiveEvent.Id,
            ArgumentId = context.Event.ArgumentId,
            Comparison = context.Event.Comparison,
            Value = context.Event.Value
        }, cancellationToken);
    }

    public async Task HandleAsync(IEventHandlerContext<ConstraintRemoved> context, CancellationToken cancellationToken = default)
    {
        var constraint = await _dbContext.AlgorithmConstraints.FindAsync([context.Event.Id], cancellationToken);

        if (constraint == null)
        {
            return;
        }

        _dbContext.AlgorithmConstraints.Remove(constraint);

        await _dbContext.SaveChangesAsync(cancellationToken);
        await _bus.PublishAsync(new Messages.v1.AlgorithmConstraintRemoved
        {
            AlgorithmId = context.PrimitiveEvent.Id,
            Id = context.Event.Id
        }, cancellationToken);
    }
}
