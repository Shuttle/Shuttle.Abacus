using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.FactorAnswer
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
