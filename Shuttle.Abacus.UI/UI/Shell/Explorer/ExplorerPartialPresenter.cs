using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Shuttle.Abacus.UI.Core.Clipboard;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.Resources;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.Explorer;
using Shuttle.Abacus.UI.Messages.Resources;
using Shuttle.Abacus.UI.Messages.WorkItem;
using Shuttle.Abacus.UI.Navigation;
using Message = Shuttle.Abacus.UI.Core.Messaging.Message;

namespace Shuttle.Abacus.UI.UI.Shell.Explorer
{
    public class ExplorerPartialPresenter : IExplorerPartialPresenter
    {
        private readonly IClipboard clipboard;
        private readonly Dictionary<Guid, Dispatcher> dispatchedMessages = new Dictionary<Guid, Dispatcher>();
        private readonly IExplorerRootItemOrderProvider explorerRootItemOrderProvider;

        private readonly IMessageBus messageBus;
        private readonly ISession session;
        private readonly IExplorerPartialView view;

        public ExplorerPartialPresenter(
            IExplorerPartialView view,
            IMessageBus messageBus,
            IExplorerRootItemOrderProvider explorerRootItemOrderProvider,
            ISession session,
            IClipboard clipboard)
        {
            Guard.AgainstNull(view, "view");
            Guard.AgainstNull(messageBus, "messageBus");

            messageBus.AddSubscriber(this);

            this.explorerRootItemOrderProvider = explorerRootItemOrderProvider;
            this.session = session;
            this.clipboard = clipboard;

            this.view = view;
            this.messageBus = messageBus;

            view.AttachPresenter(this);
        }

        public void AddItem(Resource resource)
        {
            AddItem(resource, null);
        }

        public List<Resource> Find(Guid key)
        {
            var nodes = view.Find(key);

            var result = new List<Resource>();

            foreach (var node in nodes)
            {
                var item = node.Tag as Resource;

                if (item != null)
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public void PopulateNode(TreeNode node)
        {
            var item = (Resource) node.Tag;

            if (item == null)
            {
                return;
            }

            if (!item.Populated)
            {
                view.RemoveWaitIndicator(node);

                var message = new PopulateResourceMessage(item, view.RelatedItems(node));

                messageBus.Publish(message);

                AddItems(message.Resources, node);

                item.Populated = true;
            }

            messageBus.Publish(new ResourceSelectedMessage(item, view.RelatedItems(node)));
        }

        public void ContextMenuRequested(Resource resource, Point location)
        {
            view.ShowContextMenu(GetContextMenuStrip(resource), location);
        }

        public void HandleMessage(ResourceRefreshItemMessage message)
        {
            view.Refresh(message.Item);
        }

        public void HandleMessage(RefreshWorkItemDispatcherMessage message)
        {
            if (message.WorkItem == null ||
                !dispatchedMessages.ContainsKey(message.WorkItem.Initiator.WorkItemInitiatorId))
            {
                return;
            }

            var item = dispatchedMessages[message.WorkItem.Initiator.WorkItemInitiatorId].Item;

            if (!message.WorkItem.Initiator.RefreshOwner)
            {
                view.Refresh(item);
            }
            else
            {
                var nodes = view.Find(item);

                foreach (var node in nodes)
                {
                    var refresh = node.Parent;

                    if (refresh != null)
                    {
                        view.Refresh((Resource) refresh.Tag);
                    }
                }
            }
        }

        public void HandleMessage(RefreshWorkItemDispatcherTextMessage message)
        {
            if (!dispatchedMessages.ContainsKey(message.DispatchedMessageId))
            {
                return;
            }

            var item = dispatchedMessages[message.DispatchedMessageId].Item;

            var refresh = new ResourceRefreshItemTextMessage(item);

            messageBus.Publish(refresh);

            view.RefreshText(item);
        }

        public void HandleMessage(SummaryViewActivatedMessage message)
        {
            if (view.SelectedNode == null)
            {
                return;
            }

            PopulateNode(view.SelectedNode);
        }

        private void AddItem(Resource item, TreeNode parentNode)
        {
            view.AddImage(item);

            view.AddNode(item, parentNode);
        }

        private void AddItems(List<Resource> items, TreeNode node)
        {
            items.ForEach(candidate =>
                {
                    if (candidate.ResourceKey.Permission.IsSatisfiedBy(session.Permissions))
                    {
                        AddItem(candidate, node);
                    }
                });
        }

        private ContextMenuStrip GetContextMenuStrip(Resource resource)
        {
            var strip = new ContextMenuStrip();

            ToolStripMenuItem menuItem;

            var message = new ResourceMenuRequestMessage(resource, view.RelatedItems(), view.UpstreamItems());

            messageBus.Publish(message);

            var toolTipText = view.Breadcrumb();

            foreach (var navigationItem in message.NavigationItems)
            {
                navigationItem.Message.ToolTipText = toolTipText;

                var holder = navigationItem.Message as IResourceHolder;

                if (holder != null)
                {
                    holder.AssignResource(resource);
                }

                menuItem = new ToolStripMenuItem(navigationItem.Text, navigationItem.Image)
                           {
                               Tag = navigationItem
                           };

                menuItem.Click += MenuItemClicked;

                strip.Items.Add(menuItem);
            }

            menuItem = new ToolStripMenuItem("&Refresh")
                       {
                           Image = ResourceItem.GetImage("RefreshSmall")
                       };

            menuItem.Click += RefreshClicked;

            strip.Items.Add(menuItem);

            return strip;
        }

        private void MenuItemClicked(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem) sender;

            var navigationItem = menuItem.Tag as NavigationItem;

            if (navigationItem == null)
            {
                messageBus.Publish(
                    new ResultNotificationMessage(
                        Result.Create().AddFailureMessage("No NavigationItem has been assigned to the menu item.")));

                return;
            }

            var holder = navigationItem.Message as IResourceHolder;

            if (holder != null)
            {
                clipboard.Replace(holder.Resource);

                return;
            }

            RegisterDispatchedMessage(navigationItem.Message);

            messageBus.Publish(navigationItem.Message);
        }

        private void RegisterDispatchedMessage(Message message)
        {
            if (dispatchedMessages.ContainsKey(message.MessageId))
            {
                dispatchedMessages.Remove(message.MessageId);
            }

            dispatchedMessages.Add(message.MessageId, new Dispatcher(view.SelectedItem));

            OptimizeDispatchedMessages();
        }

        private void OptimizeDispatchedMessages()
        {
            if (dispatchedMessages.Count < 1000)
            {
                return;
            }

            var list = new List<Guid>();

            dispatchedMessages.ForEach(item =>
                {
                    if (item.Value.Registration <
                        DateTime.Now.AddMinutes(-2))
                    {
                        list.Add(item.Key);
                    }
                });

            list.ForEach(guid => dispatchedMessages.Remove(guid));
        }

        private void RefreshClicked(object sender, EventArgs e)
        {
            view.Refresh(view.SelectedItem);
        }

        public void Initialize()
        {
            view.Clear();

            var message = new ExplorerInitializeMessage();

            messageBus.Publish(message);

            explorerRootItemOrderProvider.OrderedList(message.Items).ForEach(AddItem);
        }

        private class Dispatcher
        {
            public Dispatcher(Resource item)
            {
                Item = item;
                Registration = DateTime.Now;
            }

            public Resource Item { get; private set; }
            public DateTime Registration { get; private set; }
        }
    }
}
