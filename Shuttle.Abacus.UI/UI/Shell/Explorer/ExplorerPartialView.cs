using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.Shell.Core.Resources;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.UI.Shell.Explorer
{
    public class ExplorerPartialView : IExplorerPartialView
    {
        private const string WaitIndicatorImage = "Hourglass";

        private readonly TreeView explorer;

        private IExplorerPartialPresenter presenter;

        public ExplorerPartialView(TreeView explorer)
        {
            this.explorer = explorer;

            AddImage(WaitIndicatorImage);

            explorer.AfterSelect += AfterSelect;
            explorer.NodeMouseClick += NodeMouseClick;
            explorer.AfterExpand += AfterExpand;
        }

        public TreeNodeCollection SelectedNodes
        {
            get
            {
                TreeNodeCollection result = null;

                Invoke(() => result = explorer.SelectedNode != null
                                          ? explorer.SelectedNode.Nodes
                                          : explorer.Nodes);

                return result;
            }
        }

        public Resource SelectedItem => explorer.SelectedNode != null
            ? explorer.SelectedNode.Tag != null
                ? explorer.SelectedNode.Tag as Resource
                : null
            : null;

        public Resource OwnerItem(TreeNode node)
        {
            return node == null
                       ? null
                       : (node.Parent == null
                              ? null
                              : node.Parent.Tag as Resource);
        }

        public TreeNode SelectedNode => explorer.SelectedNode;

        public TreeNode[] Find(Guid itemKey)
        {
            return explorer.Nodes.Find(itemKey.ToString(), true);
        }

        public TreeNode[] Find(Resource item)
        {
            return Find(item.Key);
        }

        public void AddImage(Resource item)
        {
            Invoke(() =>
                {
                    if (!item.HasImage || explorer.ImageList == null ||
                        explorer.ImageList.Images.ContainsKey(item.ImageResource.Key))
                    {
                        return;
                    }

                    AddImage(item.ImageResource.Key);
                });
        }

        public void AttachPresenter(IExplorerPartialPresenter explorerPartialPresenter)
        {
            presenter = explorerPartialPresenter;
        }

        public void AddWaitIndicator(TreeNode node)
        {
            if (node == null)
            {
                return;
            }

            Invoke(() => node.Nodes.Add(string.Empty, "(waiting)", WaitIndicatorImage, WaitIndicatorImage));
        }

        public void RemoveWaitIndicator(TreeNode node)
        {
            Guard.AgainstNull(node, "node");

            node.Nodes.Clear();
        }

        public TreeNode AddNode(Resource item, TreeNode parentNode)
        {
            var nodes = parentNode != null
                            ? parentNode.Nodes
                            : SelectedNodes;

            TreeNode node = null;

            Invoke(() =>
                {
                    node = nodes.Add(item.Key.ToString(), item.Text, item.ImageResource.Key,
                                     item.ImageResource.Key);
                });

            node.Tag = item;

            if (!item.IsLeaf)
            {
                AddWaitIndicator(node);
            }

            return node;
        }

        public void ShowContextMenu(ContextMenuStrip contextMenuStrip, Point location)
        {
            contextMenuStrip.Show(explorer, location);
        }

        public void Refresh(Resource item)
        {
            if (item == null)
            {
                return;
            }

            item.Refresh();

            Invoke(() =>
                {
                    foreach (var node in Find(item))
                    {
                        AddWaitIndicator(node);

                        presenter.PopulateNode(node);
                    }
                }
                );
        }

        public ResourceCollection RelatedItems(TreeNode node)
        {
            var result = new ResourceCollection();

            while (node != null)
            {
                var item = node.Tag as Resource;

                if (item != null &&
                    item.Type == Resource.ResourceType.Item)
                {
                    result.Add(item);
                }

                node = node.Parent;
            }

            return result;
        }

        public ResourceCollection RelatedItems()
        {
            return RelatedItems(SelectedNode.Parent);
        }

        public void RefreshText(Resource item)
        {
            Invoke(() =>
            {
                foreach (var node in Find(item))
                {
                    node.Text = item.Text;
                }
            });
        }

        public void Clear()
        {
            Invoke(() => explorer.Nodes.Clear());
        }

        public ResourceCollection UpstreamItems()
        {
            return UpstreamItems(SelectedNode.Parent);
        }

        public string Breadcrumb()
        {
            var result = new StringBuilder();

            var node = SelectedNode;

            while (node != null)
            {
                var item = node.Tag as Resource;

                if (item != null &&
                    item.Type == Resource.ResourceType.Item)
                {
                    result.Insert(0, string.Format("{0}{1}", item.Text, result.Length > 0 ? " | " : string.Empty));
                }

                node = node.Parent;
            }

            return result.ToString();
        }

        public ResourceCollection UpstreamItems(TreeNode node)
        {
            var result = new ResourceCollection();

            while (node != null)
            {
                var item = node.Tag as Resource;

                if (item != null)
                {
                    result.Add(item);
                }

                node = node.Parent;
            }

            return result;
        }

        private void AfterExpand(object sender, TreeViewEventArgs e)
        {
            presenter.PopulateNode(e.Node);
        }

        private void NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button !=
                MouseButtons.Right)
            {
                return;
            }

            if (explorer.GetNodeAt(e.Location) !=
                explorer.SelectedNode)
            {
                explorer.SelectedNode = e.Node;

                presenter.PopulateNode(e.Node);
            }

            presenter.ContextMenuRequested((Resource)e.Node.Tag, e.Location);
        }

        private void AfterSelect(object sender, TreeViewEventArgs e)
        {
            presenter.PopulateNode(e.Node);
        }

        private void AddImage(string imageKey)
        {
            var image = ResourceItem.GetImage(imageKey);

            if (image != null)
            {
                explorer.ImageList.Images.Add(imageKey, image);
            }
        }

        private void Invoke(Action action)
        {
            if (explorer.InvokeRequired)
            {
                explorer.Invoke(new MethodInvoker(action));
            }
            else
            {
                action.Invoke();
            }
        }
    }
}
