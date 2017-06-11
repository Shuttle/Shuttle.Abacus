using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Events.Matrix.v1;
using Shuttle.Core.Infrastructure;
using Shuttle.Recall;

namespace Shuttle.Abacus.Server.EventHandlers
{
    public class MatrixHandler :
        IEventHandler<Registered>,
        IEventHandler<ConstraintAdded>,
        IEventHandler<ElementAdded>
    {
        private readonly IMatrixQuery _query;

        public MatrixHandler(IMatrixQuery query)
        {
            Guard.AgainstNull(query, "query");

            _query = query;
        }

        public void ProcessEvent(IEventHandlerContext<Registered> context)
        {
            var registered = context.Event;

            _query.Registered(context.PrimitiveEvent.Id, registered.Name, registered.ColumnArgumentName, registered.RowArgumentName, registered.ValueType);
        }

        public void ProcessEvent(IEventHandlerContext<ConstraintAdded> context)
        {
            var constraintAdded = context.Event;

            _query.ConstraintAdded(context.PrimitiveEvent.Id, constraintAdded.SequenceNumber, constraintAdded.Axis, constraintAdded.Comparison, constraintAdded.Value);
        }

        public void ProcessEvent(IEventHandlerContext<ElementAdded> context)
        {
            var elementAdded = context.Event;

            _query.ElementAdded(context.PrimitiveEvent.Id, elementAdded.Column, elementAdded.Row, elementAdded.Value);
        }
    }
}