using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Shell.Core.WorkItem;

namespace Shuttle.Abacus.Shell.Core.Messaging
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
