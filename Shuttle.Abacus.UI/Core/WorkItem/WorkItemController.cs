using System;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Messages.WorkItem;

namespace Shuttle.Abacus.UI.Core.WorkItem
{
    public abstract class WorkItemController : IWorkItemController
    {
        public IBus Bus { get; set; }
        public IWorkItem WorkItem { get; private set; }
        public IMessageBus MessageBus { get; set; }

        public void AssignWorkItem(IWorkItem workItem)
        {
            Guard.AgainstReassignment(WorkItem, "WorkItem");

            WorkItem = workItem;
        }

        protected void Send(IMessage command)
        {
            SetWorkItemWaiting();

            Bus.Send(command).Register(ar => ReplyCallback(ar, () => { }, true), command);
        }

        private void SetWorkItemWaiting()
        {
            if (WorkItem == null)
            {
                return;
            }

            WorkItem.Waiting();
        }

        protected void Send(IMessage command, Action action)
        {
            SetWorkItemWaiting();

            Bus.Send(command).Register(ar => ReplyCallback(ar, action, true), command);
        }

        protected void SendNoComplete(IMessage command)
        {
            SetWorkItemWaiting();

            Bus.Send(command).Register(ar => ReplyCallback(ar, () => { }, false), command);
        }

        protected void SendNoComplete(IMessage command, Action action)
        {
            SetWorkItemWaiting();

            Bus.Send(command).Register(ar => ReplyCallback(ar, action, false), command);
        }

        protected void SendNoCallback(IMessage command)
        {
            Bus.Send(command);
        }

        private void ReplyCallback(IAsyncResult ar, Action action, bool complete)
        {
            if (WorkItem!= null)
            {
                WorkItem.Ready();
            }

            var result = ar.AsyncState as CompletionResult;

            if (result == null)
            {
                return;
            }

            if (result.Messages == null)
            {
                return;
            }

            if (result.Messages.Length == 0)
            {
                return;
            }

            if (result.State == null)
            {
                return;
            }

            var reply = result.Messages[0] as ReplyMessage;

            if (reply == null )
            {
                return;
            }

            if (reply.Result.HasMessages)
            {
                MessageBus.Publish(reply.Result);
            }

            if (!reply.Result.OK)
            {
                return;
            }

            action.Invoke();

            if (!complete || WorkItem == null)
            {
                return;
            }

            MessageBus.Publish(new RefreshWorkItemDispatcherMessage(WorkItem));

            MessageBus.Publish(new WorkItemCompletedMessage(WorkItem));
        }

        public void Dispose()
        {
            MessageBus.RemoveSubscriber(this);
        }
    }
}
