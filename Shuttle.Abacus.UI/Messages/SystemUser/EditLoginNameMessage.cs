using System;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class EditLoginNameMessage : Message
    {
        public EditLoginNameMessage()
        {
        }

        public EditLoginNameMessage(Guid systemUserId, string loginName)
        {
            SystemUserId = systemUserId;
            LoginName = loginName;
        }

        public Guid SystemUserId { get; set; }
        public string LoginName { get; set; }

        public override IPermission RequiredPermission
        {
            get { return Permissions.SystemUser; }
        }
    }
}
