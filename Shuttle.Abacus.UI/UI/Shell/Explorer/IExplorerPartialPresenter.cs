using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.Resources;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.Resources;
using Shuttle.Abacus.UI.Messages.WorkItem;

namespace Shuttle.Abacus.UI.UI.Shell.Explorer
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
