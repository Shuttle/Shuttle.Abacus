using Shuttle.Abacus.Messages.v1;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.DecimalTable;
using Shuttle.Abacus.UI.Messages.WorkItem;
using Shuttle.Esb;

namespace Shuttle.Abacus.UI.UI.Matrix
{
    public class MatrixController : WorkItemController, IMatrixController
    {
        public MatrixController(IServiceBus serviceBus, IMessageBus messageBus,
            ICallbackRepository callbackRepository)
            : base(serviceBus, messageBus, callbackRepository)
        {
        }

        public void HandleMessage(NewMatrixMessage message)
        {
            if (!WorkItem.PresentationValid())
            {
                return;
            }

            var view = WorkItem.GetView<IMatrixView>();

            if (view.HasInvalidDecimalTable())
            {
                WorkItem.GetPresenter<IMatrixPresenter>().ShowInvalidDecimalTableMessage();

                return;
            }

            var command = new CreateMatrixCommand
            {
                DecimalTableName = view.DecimalTableNameValue,
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

            if (view.HasInvalidDecimalTable())
            {
                WorkItem.GetPresenter<IMatrixPresenter>().ShowInvalidDecimalTableMessage();

                return;
            }

            var command = new UpdateMatrixCommand
            {
                MatrixId = message.MatrixId,
                DecimalTableName = view.DecimalTableNameValue,
                RowArgumentName = view.RowArgumentModel.Name,
                ColumnArgumentName = view.ColumnArgumentModel.Name,
                Elements = view.DecimalValues()
            };

            Send(command,
                () =>
                    MessageBus.Publish(new RefreshWorkItemDispatcherTextMessage(WorkItem.Initiator.WorkItemInitiatorId)));
        }
    }
}