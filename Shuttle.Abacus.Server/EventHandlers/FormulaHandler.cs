using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Events.Formula.v1;
using Shuttle.Core.Infrastructure;
using Shuttle.Recall;

namespace Shuttle.Abacus.Server.EventHandlers
{
    public class FormulaHandler :
        IEventHandler<Registered>,
        IEventHandler<Removed>
    {
        private readonly IFormulaQuery _query;

        public FormulaHandler(IFormulaQuery query)
        {
            Guard.AgainstNull(query, "query");

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
    }
}