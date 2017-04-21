using System;
using System.Linq;
using System.Windows.Forms;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class SimpleListPresenter : Presenter<ISimpleListView, SimpleListModel>, ISimpleListPresenter
    {
        public SimpleListPresenter(ISimpleListView view)
            : base(view)
        {
            Text = "List";

            DontTrackChanges();
        }

        public void DoubleClick()
        {
            if (!View.HasSelectedItem)
            {
                return;
            }

            MessageBus.Publish(new DoubleClickMessage(), WorkItem);
        }

        public void Refresh()
        {
            if (Model == null)
            {
                return;
            }

            View.PopulateListView(Model);
        }

        public override void OnInitialize()
        {
            base.OnInitialize();

            Guard.AgainstNullDependency(Model, "Model");

            Refresh();

            View.FullRowSelect = true;
            
            if (Model.HasCheckBoxes)
            {
                View.ShowCheckboxes();
            }

            SetupMenu();

            MessageBus.Publish(new ListReadyMessage(), WorkItem);
        }

        private void SetupMenu()
        {
            if (MergedNavigationItems().Count() == 0)
            {
                return;
            }

            var strip = new ContextMenuStrip();

            ToolStripMenuItem menuItem;

            foreach (var navigationItem in MergedNavigationItems())
            {
                menuItem = new ToolStripMenuItem(navigationItem.Text, navigationItem.Image) { Tag = navigationItem };

                menuItem.Click += MenuItemClicked;

                strip.Items.Add(menuItem);
            }

            View.AssignContextMenuStrip(strip);
        }

        private void MenuItemClicked(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;

            var navigationItem = menuItem.Tag as NavigationItem;

            if (navigationItem == null)
            {
                MessageBus.Publish(new ResultNotificationMessage(Result.Create().AddFailureMessage("No NavigationItem has been assigned to the menu item.")));

                return;
            }

            MessageBus.Publish(navigationItem.Message, WorkItem);
        }
    }
}
