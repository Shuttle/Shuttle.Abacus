using Shuttle.Abacus.Events.Argument.v1;
using Shuttle.Abacus.SqlServer;
using Shuttle.Contract;
using Shuttle.Hopper;
using Shuttle.Recall;

namespace Shuttle.Abacus.EventProcessing.v1.EventHandlers;

public class ArgumentHandler(AbacusDbContext dbContext, IBus bus) :
    IEventHandler<Registered>,
    IEventHandler<Renamed>,
    IEventHandler<Removed>,
    IEventHandler<ValueAdded>,
    IEventHandler<ValueRemoved>
{
    private readonly AbacusDbContext _dbContext = Guard.AgainstNull(dbContext);
    private readonly IBus _bus = Guard.AgainstNull(bus);

    public async Task HandleAsync(IEventHandlerContext<Registered> context, CancellationToken cancellationToken = default)
    {
        _dbContext.Arguments.Add(new()
        {
            Id = context.PrimitiveEvent.Id,
            Name = context.Event.Name,
            DataTypeName = context.Event.DataTypeName
        });

        await _dbContext.SaveChangesAsync(cancellationToken);
        await _bus.PublishAsync(new Messages.v1.ArgumentRegistered
        {
            Id = context.PrimitiveEvent.Id,
            Name = context.Event.Name,
            DataTypeName = context.Event.DataTypeName
        }, cancellationToken);
    }

    public async Task HandleAsync(IEventHandlerContext<Renamed> context, CancellationToken cancellationToken = default)
    {
        var argument = await _dbContext.Arguments.FindAsync([context.PrimitiveEvent.Id], cancellationToken);

        if (argument == null)
        {
            return;
        }

        argument.Name = context.Event.Name;

        await _dbContext.SaveChangesAsync(cancellationToken);
        await _bus.PublishAsync(new Messages.v1.ArgumentRenamed
        {
            Id = context.PrimitiveEvent.Id,
            Name = context.Event.Name
        }, cancellationToken);
    }

    public async Task HandleAsync(IEventHandlerContext<Removed> context, CancellationToken cancellationToken = default)
    {
        var argument = await _dbContext.Arguments.FindAsync([context.PrimitiveEvent.Id], cancellationToken);

        if (argument == null)
        {
            return;
        }

        _dbContext.Arguments.Remove(argument);

        await _dbContext.SaveChangesAsync(cancellationToken);
        await _bus.PublishAsync(new Messages.v1.ArgumentRemoved
        {
            Id = context.PrimitiveEvent.Id
        }, cancellationToken);
    }

    public async Task HandleAsync(IEventHandlerContext<ValueAdded> context, CancellationToken cancellationToken = default)
    {
        _dbContext.ArgumentValues.Add(new()
        {
            ArgumentId = context.PrimitiveEvent.Id,
            Value = context.Event.Value
        });

        await _dbContext.SaveChangesAsync(cancellationToken);
        await _bus.PublishAsync(new Messages.v1.ArgumentValueAdded
        {
            ArgumentId = context.PrimitiveEvent.Id,
            Value = context.Event.Value
        }, cancellationToken);
    }

    public async Task HandleAsync(IEventHandlerContext<ValueRemoved> context, CancellationToken cancellationToken = default)
    {
        var value = await _dbContext.ArgumentValues.FindAsync([context.PrimitiveEvent.Id, context.Event.Value], cancellationToken);

        if (value == null)
        {
            return;
        }

        _dbContext.ArgumentValues.Remove(value);

        await _dbContext.SaveChangesAsync(cancellationToken);
        await _bus.PublishAsync(new Messages.v1.ArgumentValueRemoved
        {
            ArgumentId = context.PrimitiveEvent.Id,
            Value = context.Event.Value
        }, cancellationToken);
    }
}
