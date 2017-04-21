using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Abacus.UI
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
