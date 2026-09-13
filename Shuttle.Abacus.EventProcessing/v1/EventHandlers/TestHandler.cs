using Shuttle.Abacus.Events.Test.v1;
using Shuttle.Abacus.SqlServer;
using Shuttle.Contract;
using Shuttle.Hopper;
using Shuttle.Recall;

namespace Shuttle.Abacus.EventProcessing.v1.EventHandlers;

public class TestHandler(AbacusDbContext dbContext, IBus bus) :
    IEventHandler<Registered>,
    IEventHandler<Renamed>,
    IEventHandler<Removed>,
    IEventHandler<ArgumentRegistered>,
    IEventHandler<ArgumentRemoved>
{
    private readonly AbacusDbContext _dbContext = Guard.AgainstNull(dbContext);
    private readonly IBus _bus = Guard.AgainstNull(bus);

    public async Task HandleAsync(IEventHandlerContext<Registered> context, CancellationToken cancellationToken = default)
    {
        _dbContext.Tests.Add(new()
        {
            Id = context.PrimitiveEvent.Id,
            Name = context.Event.Name,
            AlgorithmId = context.Event.AlgorithmId,
            ExpectedResult = context.Event.ExpectedResult,
            ExpectedResultDataTypeName = context.Event.ExpectedResultDataTypeName,
            Comparison = context.Event.Comparison
        });

        await _dbContext.SaveChangesAsync(cancellationToken);
        await _bus.PublishAsync(new Messages.v1.TestRegistered
        {
            Id = context.PrimitiveEvent.Id,
            Name = context.Event.Name,
            AlgorithmId = context.Event.AlgorithmId,
            ExpectedResult = context.Event.ExpectedResult,
            ExpectedResultDataTypeName = context.Event.ExpectedResultDataTypeName,
            Comparison = context.Event.Comparison
        }, cancellationToken);
    }

    public async Task HandleAsync(IEventHandlerContext<Renamed> context, CancellationToken cancellationToken = default)
    {
        var test = await _dbContext.Tests.FindAsync([context.PrimitiveEvent.Id], cancellationToken);

        if (test == null)
        {
            return;
        }

        test.Name = context.Event.Name;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task HandleAsync(IEventHandlerContext<Removed> context, CancellationToken cancellationToken = default)
    {
        var test = await _dbContext.Tests.FindAsync([context.PrimitiveEvent.Id], cancellationToken);

        if (test == null)
        {
            return;
        }

        _dbContext.Tests.Remove(test);

        await _dbContext.SaveChangesAsync(cancellationToken);
        await _bus.PublishAsync(new Messages.v1.TestRemoved
        {
            Id = context.PrimitiveEvent.Id
        }, cancellationToken);
    }

    public async Task HandleAsync(IEventHandlerContext<ArgumentRegistered> context, CancellationToken cancellationToken = default)
    {
        var testArgument = await _dbContext.TestArguments.FindAsync([context.PrimitiveEvent.Id, context.Event.ArgumentId], cancellationToken);

        if (testArgument == null)
        {
            _dbContext.TestArguments.Add(new()
            {
                TestId = context.PrimitiveEvent.Id,
                ArgumentId = context.Event.ArgumentId,
                Value = context.Event.Value
            });
        }
        else
        {
            testArgument.Value = context.Event.Value;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        await _bus.PublishAsync(new Messages.v1.TestArgumentRegistered
        {
            TestId = context.PrimitiveEvent.Id,
            ArgumentId = context.Event.ArgumentId,
            Value = context.Event.Value
        }, cancellationToken);
    }

    public async Task HandleAsync(IEventHandlerContext<ArgumentRemoved> context, CancellationToken cancellationToken = default)
    {
        var testArgument = await _dbContext.TestArguments.FindAsync([context.PrimitiveEvent.Id, context.Event.ArgumentId], cancellationToken);

        if (testArgument == null)
        {
            return;
        }

        _dbContext.TestArguments.Remove(testArgument);

        await _dbContext.SaveChangesAsync(cancellationToken);
        await _bus.PublishAsync(new Messages.v1.TestArgumentRemoved
        {
            TestId = context.PrimitiveEvent.Id,
            ArgumentId = context.Event.ArgumentId
        }, cancellationToken);
    }
}
