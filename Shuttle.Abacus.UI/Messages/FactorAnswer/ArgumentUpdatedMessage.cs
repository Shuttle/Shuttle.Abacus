using System;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class ArgumentUpdatedMessage : Message
    {
        public ArgumentUpdatedMessage(Guid workItemId, Guid argumentId)
        {
            WorkItemId = workItemId;
            ArgumentId = argumentId;
        }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Argument; }
        }

        public Guid WorkItemId { get; set; }
        public Guid ArgumentId { get; private set; }
        public string Name { get; set; }
    }
}
