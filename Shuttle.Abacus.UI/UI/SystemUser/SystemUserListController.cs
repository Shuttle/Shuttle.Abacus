using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.WorkItem;
using Shuttle.Abacus.Shell.Messages.Core;
using Shuttle.Abacus.Shell.Messages.SystemUser;
using Shuttle.Abacus.Shell.UI.SimpleList;
using Shuttle.Esb;

namespace Shuttle.Abacus.Shell.UI.SystemUser
{
    public class SystemUserListController :
        WorkItemController,
        ISystemUserListController
    {
        public SystemUserListController(IServiceBus serviceBus, IMessageBus messageBus) 
            : base(serviceBus, messageBus)
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
