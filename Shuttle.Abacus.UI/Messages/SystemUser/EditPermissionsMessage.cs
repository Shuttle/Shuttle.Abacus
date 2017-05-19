using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Shell.Core.Messaging;

namespace Shuttle.Abacus.Shell.Messages.SystemUser
{
    public class EditPermissionsMessage : Message
    {
        public EditPermissionsMessage()
        {
        }

        public EditPermissionsMessage(Guid systemUserId, string loginName)
        {
            SystemUserId = systemUserId;
            LoginName = loginName;
        }

        public Guid SystemUserId { get; set; }
        public string LoginName { get; set; }

        public override IPermission RequiredPermission => Permissions.SystemUser;
    }
}
