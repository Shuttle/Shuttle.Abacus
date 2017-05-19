using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Core.Validation;
using Shuttle.Abacus.Shell.Navigation;
using Shuttle.Abacus.Shell.UI.Shell.Explorer;
using Message = Shuttle.Abacus.Shell.Core.Messaging.Message;

namespace Shuttle.Abacus.Shell.UI.Shell
{
    public partial class ShellView : Form, IShellView
    {
        private IShellPresenter shellPresenter;

        private readonly IExplorerPartialView explorerPartialView;

        public ShellView(IApplicationShell applicationShell)
        {
            InitializeComponent();

            explorerPartialView = new ExplorerPartialView(Explorer);

            applicationShell.AssignShell(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            shellPresenter.ViewReady();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            Application.Exit();
        }

        public void PopulateMenu(IEnumerable<INavigationItem> items)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => InvokePopulateMenu(items)));
            }
            else
            {
                InvokePopulateMenu(items);
            }
        }

        public void InvokePopulateMenu(IEnumerable<INavigationItem> items)
        {
            foreach (var item in items)
            {
                AddMenuItem(item, null);
            }
        }

        public void ShowContainer(UserControl container)
        {
            ShellSplitContainer.Panel2.Controls.Add(container);

            container.Dock = DockStyle.Fill;
            container.Show();
        }

        public void Busy()
        {
            Cursor.Current = Cursors.WaitCursor;
        }

        public void Ready()
        {
            Cursor.Current = Cursors.Default;
        }

        public void ShowStatus(string message)
        {
            StatusLabel.Text = message;
        }

        public IExplorerPartialView ExplorerView => explorerPartialView;

        public IPresenter IPresenter { get; private set; }

        public bool IsValid => true;

        public void AttachPresenter(IPresenter presenter)
        {
            IPresenter = presenter;

            shellPresenter = presenter as IShellPresenter;
        }

        public void AttachViewValidator(IViewValidator validator)
        {
            // not used
        }

        public void ValidateView()
        {
            // not used
        }

        public bool Confirmed(string message)
        {
            return
                MessageBox.Show(message, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }

        public void ShowView()
        {
        }

        public IViewValidationManager ViewValidationManager => new ViewValidationManager(null);

        private void AddMenuItem(INavigationItem item, ToolStripDropDownItem owner)
        {
            var menu = new ToolStripMenuItem(item.Text, item.Image);

            if (owner != null)
            {
                owner.DropDownItems.Add(menu);
            }
            else
            {
                NavigationMenuStrip.Items.Add(menu);
            }

            foreach (var sub in item.Items)
            {
                AddMenuItem(sub, menu);
            }

            menu.Tag = item.Message;
            menu.Click += MenuClicked;
        }

        private void MenuClicked(object sender, EventArgs e)
        {
            var item = sender as ToolStripItem;

            if (item == null)
            {
                return;
            }

            var message = item.Tag as Message;

            if (message == null)
            {
                return;
            }

            shellPresenter.PublishMessage(message);
        }
    }
}
