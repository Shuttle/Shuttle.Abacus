using Shuttle.Contract;
using Shuttle.Hopper;

namespace Shuttle.Abacus.Server.v1.MessageHandlers;

// Dead-code-adjacent: this async request/reply flow is ported as-is from the legacy codebase, but the current
// front-end only calls the synchronous `GET /tests/{id}/run` WebApi endpoint (backed directly by
// IExecutionService), never sends `ExecuteTest` over Hopper. Kept for out-of-process callers that want an
// async/queued test run.
public class ExecuteTestHandler(IBus bus, IExecutionService executionService, ITestRepository testRepository) : IMessageHandler<Messages.v1.ExecuteTest>
{
    private readonly IBus _bus = Guard.AgainstNull(bus);
    private readonly IExecutionService _executionService = Guard.AgainstNull(executionService);
    private readonly ITestRepository _testRepository = Guard.AgainstNull(testRepository);

    public async Task HandleAsync(Messages.v1.ExecuteTest message, CancellationToken cancellationToken = default)
    {
        if (!Enum.TryParse<ContextLogLevel>(message.LogLevel, true, out var logLevel))
        {
            logLevel = ContextLogLevel.None;
        }

        var test = await _testRepository.GetAsync(message.Id, cancellationToken);
        var executionContext = await _executionService.ExecuteAsync(test.AlgorithmId, test.ArgumentValues(), new ContextLogger(logLevel), cancellationToken);

        var response = new Messages.v1.TestExecuted
        {
            Id = test.Id,
            AlgorithmId = test.AlgorithmId,
            Log = executionContext.Logger.ToString()
        };

        if (executionContext.HasException)
        {
            response.Exception = executionContext.Exception!.Message;
        }
        else
        {
            response.Result = executionContext.GetResult();
            response.AlgorithmContext = executionContext.RootAlgorithmContext == null ? null : Map(executionContext.RootAlgorithmContext);
        }

        await _bus.SendAsync(response, builder => builder.AsReply(), cancellationToken);
    }

    private static Messages.v1.TransferObjects.AlgorithmContext Map(AlgorithmContext algorithmContext)
    {
        var result = new Messages.v1.TransferObjects.AlgorithmContext
        {
            Result = algorithmContext.Result,
            DateStarted = algorithmContext.DateStarted,
            DateCompleted = algorithmContext.DateCompleted
        };

        foreach (var usedArgumentValue in algorithmContext.UsedArgumentValues())
        {
            result.ArgumentAnswers.Add(new()
            {
                Id = usedArgumentValue.Id,
                Value = usedArgumentValue.Value
            });
        }

        foreach (var containedAlgorithmContext in algorithmContext.ContainedAlgorithmContexts())
        {
            result.AlgorithmContexts.Add(Map(containedAlgorithmContext));
        }

        return result;
    }
}
