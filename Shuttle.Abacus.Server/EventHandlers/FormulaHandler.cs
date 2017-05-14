using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Events.Formula.v1;
using Shuttle.Core.Infrastructure;
using Shuttle.Recall;

namespace Shuttle.Abacus.Server.EventHandlers
{
    public class FormulaHandler :
        IEventHandler<Registered>,
        IEventHandler<Renamed>,
        IEventHandler<Removed>,
        IEventHandler<OperationsRemoved>,
        IEventHandler<OperationAdded>
    {
        private readonly IFormulaQuery _query;

        public FormulaHandler(IFormulaQuery query)
        {
            Guard.AgainstNull(query, "query");

            _query = query;
        }

        public void ProcessEvent(IEventHandlerContext<Registered> context)
        {
            _query.Registered(context.PrimitiveEvent.Id, context.Event.Name);
        }

        public void ProcessEvent(IEventHandlerContext<Removed> context)
        {
            _query.Remove(context.PrimitiveEvent.Id);
        }

        public void ProcessEvent(IEventHandlerContext<Renamed> context)
        {
            _query.Rename(context.PrimitiveEvent.Id, context.Event.Name);
        }

        public void ProcessEvent(IEventHandlerContext<OperationsRemoved> context)
        {
            _query.RemoveOperations(context.PrimitiveEvent.Id);
        }

        public void ProcessEvent(IEventHandlerContext<OperationAdded> context)
        {
            var operationAdded = context.Event;

            _query.AddOperation(context.PrimitiveEvent.Id, operationAdded.SequenceNumber, operationAdded.Operation, operationAdded.ValueSource, operationAdded.ValueSelection);
        }
    }
}