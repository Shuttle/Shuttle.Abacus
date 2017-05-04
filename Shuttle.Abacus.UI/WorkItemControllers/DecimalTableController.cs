using Shuttle.Abacus.Messages.v1;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.DecimalTable;
using Shuttle.Abacus.UI.Messages.WorkItem;
using Shuttle.Abacus.UI.UI.DecimalTable;
using Shuttle.Abacus.UI.WorkItemControllers.Interfaces;
using Shuttle.Esb;

namespace Shuttle.Abacus.UI.WorkItemControllers
{
    public class DecimalTableController : WorkItemController, IDecimalTableController
    {
        public DecimalTableController(IServiceBus serviceBus, IMessageBus messageBus,
            ICallbackRepository callbackRepository)
            : base(serviceBus, messageBus, callbackRepository)
        {
        }

        public void HandleMessage(NewDecimalTableMessage message)
        {
            if (!WorkItem.PresentationValid())
            {
                return;
            }

            var view = WorkItem.GetView<IDecimalTableView>();

            if (view.HasInvalidDecimalTable())
            {
                WorkItem.GetPresenter<IDecimalTablePresenter>().ShowInvalidDecimalTableMessage();

                return;
            }

            var command = new CreateDecimalTableCommand
            {
                DecimalTableName = view.DecimalTableNameValue,
                RowArgumentId = view.RowArgumentModel.Id,
                ColumnArgumentId = view.ColumnArgumentModel.Id,
                DecimalValues = view.DecimalValues()
            };

            Send(command);
        }

        public void HandleMessage(EditDecimalTableMessage message)
        {
            if (!WorkItem.PresentationValid())
            {
                return;
            }

            var view = WorkItem.GetView<IDecimalTableView>();

            if (view.HasInvalidDecimalTable())
            {
                WorkItem.GetPresenter<IDecimalTablePresenter>().ShowInvalidDecimalTableMessage();

                return;
            }

            var command = new UpdateDecimalTableCommand
            {
                DecimalTableId = message.DecimalTableId,
                DecimalTableName = view.DecimalTableNameValue,
                RowArgumentId = view.RowArgumentModel.Id,
                ColumnArgumentId = view.ColumnArgumentModel.Id,
                DecimalValues = view.DecimalValues()
            };

            Send(command,
                () =>
                    MessageBus.Publish(new RefreshWorkItemDispatcherTextMessage(WorkItem.Initiator.WorkItemInitiatorId)));
        }
    }
}