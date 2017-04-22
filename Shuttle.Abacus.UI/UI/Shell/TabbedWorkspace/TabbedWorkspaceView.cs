using System;
using System.Windows.Forms;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.UI.Shell.TabbedWorkspace
{
    public partial class TabbedWorkspaceView : GenericTabbedWorkspaceView, ITabbedWorkspaceView
    {
        private IButtonControl currentAcceptButton;
        private IButtonControl currentCancelButton;

        public TabbedWorkspaceView()
        {
            InitializeComponent();

            Tabs.Enter += TabEnter;
            Tabs.Leave += TabLeave;

            AcceptButton.Left = -300;
            CancelButton.Left = -300;

            AcceptButton.Click += AcceptButtonClicked;
            CancelButton.Click += CancelButtonClicked;
        }

        public IMessageBus MessageBus { get; set; }

        public void Add(IWorkItem workItem)
        {
            Guard.AgainstNull(workItem, "presenter");

            Invoke(() =>
                {
                    var control = workItem.WorkItemPresenter.IView as UserControl;

                    if (control == null)
                    {
                        throw new NotSupportedException(string.Format(Resources.IViewNotAControl,
                                                                      workItem.WorkItemPresenter.IView.GetType().
                                                                          FullName));
                    }

                    var tab = new TabPage
                              {
                                  Text = workItem.Text,
                                  Tag = workItem,
                                  ToolTipText = workItem.ToolTipText
                              };

                    tab.Controls.Add(control);

                    control.Show();
                    control.Dock = DockStyle.Fill;

                    Tabs.TabPages.Add(tab);

                    Tabs.SelectedTab = tab;
                });
        }

        public void RemoveTab(IWorkItem workItem)
        {
            Invoke(() =>
                       {
                           var tab = FindTab(workItem);

                           if (tab == null)
                           {
                               return;
                           }

                           CloseTab(tab);
                       });
        }

        public void SetTabText(IWorkItem workItem)
        {
            Invoke(() =>
                       {
                           var tab = FindTab(workItem);

                           if (tab == null)
                           {
                               return;
                           }

                           tab.Text = workItem.Text;
                       });
        }

        public IWorkItem SelectedWorkItem
        {
            get
            {
                var tab = Tabs.SelectedTab;

                if (tab == null)
                {
                    return null;
                }

                return tab.Tag as IWorkItem;
            }
        }

        public void SetTabWaiting(IWorkItem workItem)
        {
            Invoke(() =>
                       {
                           var tab = FindTab(workItem);

                           if (tab == null)
                           {
                               return;
                           }

                           tab.Enabled = false;
                       });
        }

        public void SetTabReady(IWorkItem workItem)
        {
            Invoke(() =>
                       {
                           var tab = FindTab(workItem);

                           if (tab == null)
                           {
                               return;
                           }

                           tab.Enabled = true;
                       });
        }

        public void Show(IWorkItem workItem)
        {
            Invoke(() =>
                       {
                           TabPage tab = null;

                           Invoke(() => tab = FindTab(workItem));

                           if (tab == null)
                           {
                               return;
                           }

                           Tabs.SelectedTab = tab;
                       });
        }

        private void CancelButtonClicked(object sender, EventArgs e)
        {
            Presenter.ViewCancelled();
        }

        private void AcceptButtonClicked(object sender, EventArgs e)
        {
            Presenter.ViewAccepted();
        }

        private void CloseTab(TabPage tab)
        {
            var index = Tabs.SelectedIndex;

            tab.Dispose();

            Tabs.TabPages.Remove(tab);

            if (index > 0)
            {
                Tabs.SelectedTab = Tabs.TabPages[index - 1];
            }
        }

        private void TabLeave(object sender, EventArgs e)
        {
            ReleaseButtons();
        }

        private void ReleaseButtons()
        {
            var form = FindForm();

            if (form == null)
            {
                return;
            }

            form.AcceptButton = currentAcceptButton;
            form.CancelButton = currentCancelButton;

            currentAcceptButton = null;
            currentCancelButton = null;
        }

        private void TabEnter(object sender, EventArgs e)
        {
            CaptureButtons();
        }

        private void CaptureButtons()
        {
            var form = FindForm();

            if (form == null)
            {
                return;
            }

            currentAcceptButton = form.AcceptButton;
            currentCancelButton = form.CancelButton;

            form.AcceptButton = AcceptButton;
            form.CancelButton = CancelButton;
        }

        //private void TabMouseUp(object sender, MouseEventArgs e)
        //{
        //    var tab = (TabPage) sender;

        //    tab.Select();

        //    switch (e.Button)
        //    {
        //        case MouseButtons.Middle:
        //            {
        //                Presenter.ViewCancelled();

        //                break;
        //            }
        //    }
        //}

        private TabPage FindTab(IWorkItem workItem)
        {
            TabPage result = null;

            Invoke( () =>
                       {
                           foreach (TabPage tab in Tabs.TabPages)
                           {
                               var tabWorkItem = tab.Tag as IWorkItem;

                               if (tabWorkItem == null)
                               {
                                   continue;
                               }

                               if (tabWorkItem == workItem)
                               {
                                   result = tab;
                                   break;
                               }
                           }
                       });
            return result;

        }

        protected override void Dispose(bool disposing)
        {
            MessageBus.RemoveSubscriber(this);

            AcceptButton.Click -= AcceptButtonClicked;
            CancelButton.Click -= CancelButtonClicked;

            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }
    }

    public class GenericTabbedWorkspaceView : View<ITabbedWorkspacePresenter>
    {
    }
}
