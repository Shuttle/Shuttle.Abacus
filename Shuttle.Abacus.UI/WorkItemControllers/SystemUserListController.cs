namespace Abacus.UI
{
    public class SystemUserListController :
        WorkItemController,
        ISystemUserListController
    {
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
