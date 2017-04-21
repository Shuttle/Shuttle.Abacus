using System.Collections.Generic;
using Abacus.Messages;

namespace Abacus.UI
{
    public class SystemUserController : WorkItemController, ISystemUserController
    {
        public void HandleMessage(NewSystemUserMessage message)
        {
            if (!WorkItem.PresentationValid())
            {
                return;
            }

            var systemUserView = WorkItem.GetView<ISystemUserView>();
            var permissionsView = WorkItem.GetView<IPermissionsView>();

            var permissions = new List<string>();

            permissionsView.AssignedPermissions.ForEach(permission => permissions.Add(permission.Identifier));

            var command = new CreateSystemUserCommand
                              {
                                  LoginName = systemUserView.LoginNameValue,
                                  PermissionIdentifiers = permissions
                              };

            Send(command);
        }

        public void HandleMessage(EditPermissionsMessage message)
        {
            var permissionsView = WorkItem.GetView<IPermissionsView>();

            var permissions = new List<string>();

            permissionsView.AssignedPermissions.ForEach(permission => permissions.Add(permission.Identifier));

            Send(new SetPermissionsCommand
            {
                SystemUserId = message.SystemUserId,
                PermissionIdentifiers = permissions
            });
        }

        public void HandleMessage(EditLoginNameMessage message)
        {
            var systemUserView = WorkItem.GetView<ISystemUserView>();

            Send(new ChangeLoginNameCommand
            {
                SystemUserId = message.SystemUserId,
                NewLoginName = systemUserView.LoginNameValue
            });
        }
    }
}
