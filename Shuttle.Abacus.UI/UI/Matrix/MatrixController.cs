using Shuttle.Abacus.Messages.v1;
using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.WorkItem;
using Shuttle.Abacus.Shell.Messages.DecimalTable;
using Shuttle.Esb;

namespace Shuttle.Abacus.Shell.UI.Matrix
{
    public class MatrixController : WorkItemController, IMatrixController
    {
        public MatrixController(IServiceBus serviceBus, IMessageBus messageBus)
            : base(serviceBus, messageBus)
        {
        }

        public void HandleMessage(RegisterMatrixMessage message)
        {
            if (!WorkItem.PresentationValid())
            {
                return;
            }

            var view = WorkItem.GetView<IMatrixView>();

            if (view.HasInvalidMatrix())
            {
                WorkItem.GetPresenter<IMatrixPresenter>().ShowInvalidMatrixMessage();

                return;
            }

            var command = new RegisterMatrixCommand
            {
                Name = view.MatrixNameValue,
                RowArgumentName = view.RowArgumentModel.Name,
                ColumnArgumentName = view.HasColumnArgument ? view.ColumnArgumentModel.Name : string.Empty,
                ValueType = view.ValueTypeValue,
                Constraints = view.Constraints(),
                Elements = view.Elements()
            };

            Send(command);
        }
    }
}