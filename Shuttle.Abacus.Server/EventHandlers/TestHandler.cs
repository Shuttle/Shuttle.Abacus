using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Events.Test.v1;
using Shuttle.Core.Contract;
using Shuttle.Recall;

namespace Shuttle.Abacus.Server.EventHandlers
{
    public class TestHandler :
        IEventHandler<Registered>,
        IEventHandler<ArgumentSet>,
        IEventHandler<Renamed>,
        IEventHandler<Removed>
    {
        private readonly ITestQuery _query;

        public TestHandler(ITestQuery query)
        {
            Guard.AgainstNull(query, "query");

            _query = query;
        }

        public void ProcessEvent(IEventHandlerContext<ArgumentSet> context)
        {
            _query.SetArgumentValue(context.PrimitiveEvent.Id, context.Event.ArgumentName, context.Event.Value);
        }

        public void ProcessEvent(IEventHandlerContext<Registered> context)
        {
            var data = context.Event;

            _query.Register(context.PrimitiveEvent.Id, data.Name, data.FormulaName, data.ExpectedResult,
                data.ExpectedResultType, data.Comparison);
        }

        public void ProcessEvent(IEventHandlerContext<Removed> context)
        {
            _query.Remove(context.PrimitiveEvent.Id);
        }

        public void ProcessEvent(IEventHandlerContext<Renamed> context)
        {
            _query.Rename(context.PrimitiveEvent.Id, context.Event.Name);
        }
    }
}