using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.WorkItem;

namespace Shuttle.Abacus.UI.Core.Messaging
{
    public abstract class Message : IWorkItemInitiator
    {
        protected Message()
        {
            MessageId = Guid.NewGuid();
            RefreshOwner = false;
        }

        public Guid MessageId { get; private set; }

        public abstract IPermission RequiredPermission { get; }

        public Guid WorkItemInitiatorId => MessageId;

        public bool RefreshOwner { get; protected set; }

        public IWorkItemInitiator WithRefreshOwner()
        {
            RefreshOwner = true;

            return this;
        }

        public string ToolTipText { get; set; }
    }
}
