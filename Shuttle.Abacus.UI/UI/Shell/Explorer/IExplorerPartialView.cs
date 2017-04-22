using System;
using System.Drawing;
using System.Windows.Forms;
using Shuttle.Abacus.UI.Core.Resources;

namespace Shuttle.Abacus.UI.UI.Shell.Explorer
{
    public interface IExplorerPartialView
    {
        TreeNodeCollection SelectedNodes { get; }
        Resource SelectedItem { get; }
        Resource OwnerItem(TreeNode node);
        TreeNode SelectedNode { get; }
        TreeNode[] Find(Guid itemKey);
        TreeNode[] Find(Resource item);
        void AddImage(Resource item);
        void AttachPresenter(IExplorerPartialPresenter explorerPartialPresenter);
        void AddWaitIndicator(TreeNode node);
        void RemoveWaitIndicator(TreeNode node);
        TreeNode AddNode(Resource item, TreeNode node);
        void ShowContextMenu(ContextMenuStrip contextMenuStrip, Point location);
        void Refresh(Resource item);
        ResourceCollection RelatedItems(TreeNode node);
        ResourceCollection RelatedItems();
        void RefreshText(Resource item);
        void Clear();
        ResourceCollection UpstreamItems();
        string Breadcrumb();
    }
}
