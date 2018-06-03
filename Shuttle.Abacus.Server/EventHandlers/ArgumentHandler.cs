using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Events.Argument.v1;
using Shuttle.Core.Contract;
using Shuttle.Recall;

namespace Shuttle.Abacus.Server.EventHandlers
{
    public class ArgumentHandler :
        IEventHandler<Registered>,
        IEventHandler<Renamed>,
        IEventHandler<Removed>,
        IEventHandler<ValueAdded>,
        IEventHandler<ValueRemoved>
    {
        private readonly IArgumentQuery _query;

        public ArgumentHandler(IArgumentQuery query)
        {
            Guard.AgainstNull(query, nameof(query));

            _query = query;
        }

        public void ProcessEvent(IEventHandlerContext<Registered> context)
        {
            _query.Registered(context.PrimitiveEvent, context.Event);
        }

        public void ProcessEvent(IEventHandlerContext<Removed> context)
        {
            _query.Removed(context.PrimitiveEvent, context.Event);
        }

        public void ProcessEvent(IEventHandlerContext<Renamed> context)
        {
            _query.Renamed(context.PrimitiveEvent, context.Event);
        }

        public void ProcessEvent(IEventHandlerContext<ValueAdded> context)
        {
            _query.ValueAdded(context.PrimitiveEvent, context.Event);
        }

        public void ProcessEvent(IEventHandlerContext<ValueRemoved> context)
        {
            _query.ValueRemoved(context.PrimitiveEvent, context.Event);
        }
    }
}