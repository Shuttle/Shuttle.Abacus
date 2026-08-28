using Microsoft.Extensions.Options;
using Shuttle.Hopper;
using Shuttle.Mediator;
using Shuttle.Recall;

namespace Shuttle.Abacus.WebApi;

public class MessageDispatcher(IOptions<RecallOptions> recallOptions, IMediator mediator, IBus bus)
{
    public Task DispatchAsync<THopperMessage, TParticipantMessage>(Func<THopperMessage> hopperMessage, Func<TParticipantMessage> participantMessage, CancellationToken cancellationToken = default)
        where THopperMessage : class
        where TParticipantMessage : class
    {
        return recallOptions.Value.EventProcessing.ImmediateConsistency.Enabled
            ? mediator.SendAsync(participantMessage(), cancellationToken)
            : bus.SendAsync(hopperMessage(), cancellationToken);
    }
}
