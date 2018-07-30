using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Events.Matrix.v1;
using Shuttle.Core.Contract;
using Shuttle.Recall;

namespace Shuttle.Abacus.Server.EventHandlers
{
    public class MatrixHandler :
        IEventHandler<Registered>,
        IEventHandler<ConstraintRegistered>,
        IEventHandler<ElementRegistered>
    {
        private readonly IMatrixQuery _query;

        public MatrixHandler(IMatrixQuery query)
        {
            Guard.AgainstNull(query, nameof(query));

            _query = query;
        }

        public void ProcessEvent(IEventHandlerContext<ConstraintRegistered> context)
        {
            var constraintAdded = context.Event;

            _query.ConstraintRegistered(context.PrimitiveEvent.Id, constraintAdded.Axis, constraintAdded.Index, constraintAdded.Id,
                constraintAdded.Comparison, constraintAdded.Value);
        }

        public void ProcessEvent(IEventHandlerContext<ElementRegistered> context)
        {
            var elementAdded = context.Event;

            _query.ElementRegistered(context.PrimitiveEvent.Id, elementAdded.Column, elementAdded.Row, elementAdded.Id, elementAdded.Value);
        }

        public void ProcessEvent(IEventHandlerContext<Registered> context)
        {
            var registered = context.Event;

            _query.Registered(context.PrimitiveEvent.Id, registered.Name, registered.ColumnArgumentId,
                registered.RowArgumentId, registered.DataTypeName);
        }
    }
}