using Microsoft.Extensions.Options;
using Shuttle.Contract;
using Shuttle.Recall;

namespace Shuttle.Abacus.Tests;

public class FixtureEventStore : IEventStore
{
    private readonly Dictionary<Guid, EventStream> _eventStreams = new();

    public async Task<EventStream> GetAsync(Guid id, Action<EventStreamBuilder>? builder = null, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(Get(id));
    }

    public Task<IEnumerable<EventEnvelope>> SaveAsync(EventStream eventStream, Action<EventStreamBuilder>? builder = null, CancellationToken cancellationToken = default)
    {
        Guard.AgainstNull(eventStream).Commit();

        return Task.FromResult<IEnumerable<EventEnvelope>>([]);
    }

    public Task RemoveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _eventStreams.Remove(id);

        return Task.CompletedTask;
    }

    public T? FindEvent<T>(Guid id, Func<T, bool>? specification = null) where T : class
    {
        foreach (var domainEvent in Get(id).GetEvents(EventStream.EventRegistrationType.All))
        {
            if (domainEvent.Event is not T typed)
            {
                continue;
            }

            if (specification != null && !specification(typed))
            {
                continue;
            }

            return typed;
        }

        return null;
    }

    private EventStream Get(Guid id)
    {
        if (_eventStreams.TryGetValue(id, out var stream))
        {
            return stream;
        }

        var result = new EventStream(id, new EventMethodInvoker(Options.Create(new RecallOptions())));

        _eventStreams.Add(id, result);

        return result;
    }
}
