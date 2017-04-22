using System;
using System.Linq;
using System.Windows.Forms;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.SystemUser.Permissions
{
    public partial class PermissionsView : GenericPermissionsView, IPermissionsView
    {
        public PermissionsView()
        {
            InitializeComponent();
        }

        public IPermissionCollection AssignedPermissions
        {
            get
            {
                var result = new PermissionCollection();

                foreach (ListViewItem item in AssignedPermissionsListView.Items)
                {
                    var permission = item.Tag as IPermission;

                    if (permission != null)
                    {
                        result.Add(permission);
                    }
                }

                return result;
            }
            set
            {
                AssignedPermissionsListView.Items.Clear();

                foreach (var permission in value)
                {
                    var item = AssignedPermissionsListView.Items.Add(permission.Description);

                    item.Tag = permission;
                }
            }
        }

        public IPermissionCollection AvailablePermissions
        {
            set
            {
                AvailablePermissionsListView.Items.Clear();

                foreach (var permission in value)
                {
                    var item = AvailablePermissionsListView.Items.Add(permission.Description);

                    item.Tag = permission;
                }
            }
        }

        private void PermissionsView_Resize(object sender, EventArgs e)
        {
            PositionButtons();
        }

        private void PositionButtons()
        {
            ButtonPanel.Top = (PermissionPanel.Height - ButtonPanel.Height) / 2;
        }

        private void AssignButton_Click(object sender, EventArgs e)
        {
            AssignSelectedPermissions();
        }

        private void AssignSelectedPermissions()
        {
            if (AvailablePermissionsListView.SelectedItems.Count == 0)
            {
                return;
            }

            var selected = new ListViewItem[AvailablePermissionsListView.SelectedItems.Count];

            AvailablePermissionsListView.SelectedItems.CopyTo(selected, 0);

            foreach (var item in selected)
            {
                AvailablePermissionsListView.Items.Remove(item);
            }

            AssignedPermissionsListView.Items.AddRange(selected);
        }

        private void AvailablePermissionsListView_DoubleClick(object sender, EventArgs e)
        {
            AssignSelectedPermissions();
        }

        private void AssignAllButton_Click(object sender, EventArgs e)
        {
            AvailablePermissionsListView.Items.Cast<ListViewItem>().ForEach(item => item.Selected = true);

            AssignSelectedPermissions();
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            RemoveSelectedPermissions();
        }

        private void RemoveSelectedPermissions()
        {
            if (AssignedPermissionsListView.SelectedItems.Count == 0)
            {
                return;
            }

            var selected = new ListViewItem[AssignedPermissionsListView.SelectedItems.Count];

            AssignedPermissionsListView.SelectedItems.CopyTo(selected, 0);

            foreach (var item in selected)
            {
                AssignedPermissionsListView.Items.Remove(item);
            }

            AvailablePermissionsListView.Items.AddRange(selected);
        }

        private void RemoveAllButton_Click(object sender, EventArgs e)
        {
            AssignedPermissionsListView.Items.Cast<ListViewItem>().ForEach(item => item.Selected = true);

            RemoveSelectedPermissions();
        }

        private void AssignedPermissionsListView_DoubleClick(object sender, EventArgs e)
        {
            RemoveSelectedPermissions();
        }

    }

    public class GenericPermissionsView : View<IPermissionsPresenter>
    {
    }
}
