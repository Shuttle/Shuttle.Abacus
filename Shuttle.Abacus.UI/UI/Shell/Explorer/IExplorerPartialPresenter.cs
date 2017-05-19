using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.Resources;
using Shuttle.Abacus.Shell.Messages.Core;
using Shuttle.Abacus.Shell.Messages.Resources;
using Shuttle.Abacus.Shell.Messages.WorkItem;

namespace Shuttle.Abacus.Shell.UI.Shell.Explorer
{
    public interface IExplorerPartialPresenter :
        IMessageHandler<ResourceRefreshItemMessage>,
        IMessageHandler<RefreshWorkItemDispatcherMessage>,
        IMessageHandler<RefreshWorkItemDispatcherTextMessage>,
        IMessageHandler<SummaryViewActivatedMessage>
    {
        void AddItem(Resource resource);
        List<Resource> Find(Guid key);
        void PopulateNode(TreeNode node);
        void ContextMenuRequested(Resource resource, Point location);
    }
}
