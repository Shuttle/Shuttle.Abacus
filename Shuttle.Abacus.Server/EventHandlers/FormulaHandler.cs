using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Events.Formula.v1;
using Shuttle.Core.Contract;
using Shuttle.Recall;

namespace Shuttle.Abacus.Server.EventHandlers
{
    public class FormulaHandler :
        IEventHandler<Registered>,
        IEventHandler<Renamed>,
        IEventHandler<Removed>,
        IEventHandler<OperationsRemoved>,
        IEventHandler<OperationAdded>,
        IEventHandler<ConstraintsRemoved>,
        IEventHandler<ConstraintAdded>
    {
        private readonly IFormulaQuery _query;

        public FormulaHandler(IFormulaQuery query)
        {
            Guard.AgainstNull(query, nameof(query));

            _query = query;
        }

        public void ProcessEvent(IEventHandlerContext<ConstraintAdded> context)
        {
            var constraintAdded = context.Event;

            _query.AddConstraint(context.PrimitiveEvent.Id, constraintAdded.SequenceNumber,
                constraintAdded.ArgumentName, constraintAdded.Comparison, constraintAdded.Value);
        }

        public void ProcessEvent(IEventHandlerContext<ConstraintsRemoved> context)
        {
            _query.RemoveConstraints(context.PrimitiveEvent.Id);
        }

        public void ProcessEvent(IEventHandlerContext<OperationAdded> context)
        {
            var operationAdded = context.Event;

            _query.AddOperation(context.PrimitiveEvent.Id, operationAdded.SequenceNumber, operationAdded.Operation,
                operationAdded.ValueSource, operationAdded.ValueSelection);
        }

        public void ProcessEvent(IEventHandlerContext<OperationsRemoved> context)
        {
            _query.RemoveOperations(context.PrimitiveEvent.Id);
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
    }
}