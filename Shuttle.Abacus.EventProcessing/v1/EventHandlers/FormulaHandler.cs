using Shuttle.Abacus.Events.Formula.v1;
using Shuttle.Abacus.SqlServer;
using Shuttle.Contract;
using Shuttle.Hopper;
using Shuttle.Recall;

namespace Shuttle.Abacus.EventProcessing.v1.EventHandlers;

public class FormulaHandler(AbacusDbContext dbContext, IBus bus) :
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
        _dbContext.Formulas.Add(new()
        {
            Id = context.PrimitiveEvent.Id,
            Name = context.Event.Name
        });

        await _dbContext.SaveChangesAsync(cancellationToken);
        await _bus.PublishAsync(new Messages.v1.FormulaRegistered
        {
            Id = context.PrimitiveEvent.Id,
            Name = context.Event.Name
        }, cancellationToken);
    }

    public async Task HandleAsync(IEventHandlerContext<Renamed> context, CancellationToken cancellationToken = default)
    {
        var formula = await _dbContext.Formulas.FindAsync([context.PrimitiveEvent.Id], cancellationToken);

        if (formula == null)
        {
            return;
        }

        formula.Name = context.Event.Name;

        await _dbContext.SaveChangesAsync(cancellationToken);
        await _bus.PublishAsync(new Messages.v1.FormulaRenamed
        {
            Id = context.PrimitiveEvent.Id,
            Name = context.Event.Name
        }, cancellationToken);
    }

    public async Task HandleAsync(IEventHandlerContext<Removed> context, CancellationToken cancellationToken = default)
    {
        var formula = await _dbContext.Formulas.FindAsync([context.PrimitiveEvent.Id], cancellationToken);

        if (formula == null)
        {
            return;
        }

        _dbContext.Formulas.Remove(formula);

        await _dbContext.SaveChangesAsync(cancellationToken);
        await _bus.PublishAsync(new Messages.v1.FormulaRemoved
        {
            Id = context.PrimitiveEvent.Id
        }, cancellationToken);
    }

    public async Task HandleAsync(IEventHandlerContext<OperationRegistered> context, CancellationToken cancellationToken = default)
    {
        var operation = await _dbContext.FormulaOperations.FindAsync([context.PrimitiveEvent.Id, context.Event.SequenceNumber], cancellationToken);

        if (operation == null)
        {
            _dbContext.FormulaOperations.Add(new()
            {
                Id = context.Event.Id,
                FormulaId = context.PrimitiveEvent.Id,
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
        await _bus.PublishAsync(new Messages.v1.FormulaOperationRegistered
        {
            Id = context.Event.Id,
            FormulaId = context.PrimitiveEvent.Id,
            SequenceNumber = context.Event.SequenceNumber,
            Operation = context.Event.Operation,
            ValueProviderName = context.Event.ValueProviderName,
            InputParameter = context.Event.InputParameter
        }, cancellationToken);
    }

    public async Task HandleAsync(IEventHandlerContext<OperationRemoved> context, CancellationToken cancellationToken = default)
    {
        var operation = await _dbContext.FormulaOperations.FindAsync([context.PrimitiveEvent.Id, context.Event.SequenceNumber], cancellationToken);

        if (operation == null)
        {
            return;
        }

        _dbContext.FormulaOperations.Remove(operation);

        await _dbContext.SaveChangesAsync(cancellationToken);
        await _bus.PublishAsync(new Messages.v1.FormulaOperationRemoved
        {
            FormulaId = context.PrimitiveEvent.Id,
            Id = context.Event.Id
        }, cancellationToken);
    }

    public async Task HandleAsync(IEventHandlerContext<ConstraintRegistered> context, CancellationToken cancellationToken = default)
    {
        var constraint = await _dbContext.FormulaConstraints.FindAsync([context.Event.Id], cancellationToken);

        if (constraint == null)
        {
            _dbContext.FormulaConstraints.Add(new()
            {
                Id = context.Event.Id,
                FormulaId = context.PrimitiveEvent.Id,
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
        await _bus.PublishAsync(new Messages.v1.FormulaConstraintRegistered
        {
            Id = context.Event.Id,
            FormulaId = context.PrimitiveEvent.Id,
            ArgumentId = context.Event.ArgumentId,
            Comparison = context.Event.Comparison,
            Value = context.Event.Value
        }, cancellationToken);
    }

    public async Task HandleAsync(IEventHandlerContext<ConstraintRemoved> context, CancellationToken cancellationToken = default)
    {
        var constraint = await _dbContext.FormulaConstraints.FindAsync([context.Event.Id], cancellationToken);

        if (constraint == null)
        {
            return;
        }

        _dbContext.FormulaConstraints.Remove(constraint);

        await _dbContext.SaveChangesAsync(cancellationToken);
        await _bus.PublishAsync(new Messages.v1.FormulaConstraintRemoved
        {
            FormulaId = context.PrimitiveEvent.Id,
            Id = context.Event.Id
        }, cancellationToken);
    }
}
