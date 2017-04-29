using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.SystemUser;
using Shuttle.Abacus.UI.UI.List;
using Shuttle.Abacus.UI.WorkItemControllers.Interfaces;
using Shuttle.Esb;

namespace Shuttle.Abacus.UI.WorkItemControllers
{
    public class SystemUserListController :
        WorkItemController,
        ISystemUserListController
    {
        public SystemUserListController(IServiceBus serviceBus, IMessageBus messageBus, ICallbackRepository callbackRepository) 
            : base(serviceBus, messageBus, callbackRepository)
        {
        }

        public void HandleMessage(EditLoginNameMessage message)
        {
            var view = WorkItem.GetView<ISimpleListView>();

            if (!view.HasSelectedItem)
            {
                return;
            }

            message.SystemUserId = view.SelectedId;
            message.LoginName = view.SelectedText;

            MessageBus.Publish(message);
        }

        public void HandleMessage(EditPermissionsMessage message)
        {
            PublishEditPermissions();
        }

        public void HandleMessage(DoubleClickMessage message)
        {
            PublishEditPermissions();
        }

        private void PublishEditPermissions()
        {
            var view = WorkItem.GetView<ISimpleListView>();

            if (!view.HasSelectedItem)
            {
                return;
            }

            MessageBus.Publish(new EditPermissionsMessage(view.SelectedId, view.SelectedText));
        }
    }
}
