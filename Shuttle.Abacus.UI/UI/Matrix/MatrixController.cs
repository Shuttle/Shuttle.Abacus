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

        public void HandleMessage(NewMatrixMessage message)
        {
            if (!WorkItem.PresentationValid())
            {
                return;
            }

            var view = WorkItem.GetView<IMatrixView>();

            if (!view.HasValidMatrix())
            {
                WorkItem.GetPresenter<IMatrixPresenter>().ShowInvalidMatrixMessage();

                return;
            }

            var command = new CreateMatrixCommand
            {
                DecimalTableName = view.MatrixNameValue,
                RowArgumentName = view.RowArgumentModel.Name,
                ColumnArgumentName = view.ColumnArgumentModel.Name,
                Elements = view.DecimalValues()
            };

            Send(command);
        }

        public void HandleMessage(EditMatrixMessage message)
        {
            if (!WorkItem.PresentationValid())
            {
                return;
            }

            var view = WorkItem.GetView<IMatrixView>();

            if (view.HasValidMatrix())
            {
                WorkItem.GetPresenter<IMatrixPresenter>().ShowInvalidMatrixMessage();

                return;
            }

            var command = new UpdateMatrixCommand
            {
                MatrixId = message.MatrixId,
                DecimalTableName = view.MatrixNameValue,
                RowArgumentName = view.RowArgumentModel.Name,
                ColumnArgumentName = view.ColumnArgumentModel.Name,
                Elements = view.DecimalValues()
            };

            Send(command);
        }
    }
}