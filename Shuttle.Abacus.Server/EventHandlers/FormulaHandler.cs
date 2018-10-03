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
        IEventHandler<OperationRemoved>,
        IEventHandler<OperationRegistered>,
        IEventHandler<ConstraintRemoved>,
        IEventHandler<ConstraintRegistered>
    {
        private readonly IFormulaQuery _query;

        public FormulaHandler(IFormulaQuery query)
        {
            Guard.AgainstNull(query, nameof(query));

            _query = query;
        }

        public void ProcessEvent(IEventHandlerContext<ConstraintRegistered> context)
        {
            var constraintRegistered = context.Event;

            _query.AddConstraint(constraintRegistered.Id, context.PrimitiveEvent.Id, constraintRegistered.ArgumentId, constraintRegistered.Comparison, constraintRegistered.Value);
        }

        public void ProcessEvent(IEventHandlerContext<ConstraintRemoved> context)
        {
            _query.RemoveConstraint(context.Event.Id);
        }

        public void ProcessEvent(IEventHandlerContext<OperationRegistered> context)
        {
            var operationRegistered = context.Event;

            _query.AddOperation(operationRegistered.Id, context.PrimitiveEvent.Id, operationRegistered.SequenceNumber, operationRegistered.Operation,
                operationRegistered.ValueProviderName, operationRegistered.InputParameter);
        }

        public void ProcessEvent(IEventHandlerContext<OperationRemoved> context)
        {
            _query.RemoveOperation(context.PrimitiveEvent.Id);
            _query.RenumberOperations(context.Event.Id, context.Event.SequenceNumber);
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