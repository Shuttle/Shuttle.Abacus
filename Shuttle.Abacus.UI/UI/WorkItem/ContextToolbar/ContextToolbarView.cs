using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.Shell.Core.EventArgs;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Navigation;

namespace Shuttle.Abacus.Shell.UI.WorkItem.ContextToolbar
{
    public partial class ContextToolbarView : GenericContextToolbarView, IContextToolbarView
    {
        private bool first = true;

        public ContextToolbarView()
        {
            InitializeComponent();
        }

        public bool HasSelectedPresenter => PresenterListView.SelectedItems.Count > 0;

        public void AddPresenter(IPresenter presenter)
        {
            Invoke(() =>
                {
                    var view = presenter.IView;

                    var control = view as UserControl;

                    if (control == null)
                    {
                        throw new InvalidCastException(string.Format(Resources.IViewNotAControl, view.GetType().FullName));
                    }

                    if (presenter.TrackChanges)
                    {
                        HookChangeEvents(control);
                    }

                    var w = (int)(control.Width * 1.2);
                    var h = control.Height;

                    SplitContainer.Panel1.Controls.Add(control);

                    control.Dock = DockStyle.Fill;

                    var value = presenter.Text;

                    PresenterImageList.Images.Add(value, presenter.Image);

                    var item = PresenterListView.Items.Add(value, value, value);

                    item.Tag = new ItemTag(presenter, control);

                    if (first)
                    {
                        item.Selected = true;

                        ClientSize = new Size(w, h);

                        first = false;
                    }
                    else
                    {
                        var cw = ClientSize.Width;
                        var ch = ClientSize.Height;

                        ClientSize = new Size(w > cw
                                                  ? w
                                                  : cw, h > ch
                                                            ? h
                                                            : ch);
                    }

                    presenter.TextChanged += PresenterTextChanged;
                    presenter.ImageChanged += PresenterImageChanged;
                });
        }

        public bool HasChanges { get; private set; }
        public IPresenter SelectedPresenter => ((ItemTag)PresenterListView.SelectedItems[0].Tag).Presenter;

        public void ResetChanges()
        {
            HasChanges = false;
        }

        public void SelectPresenter(IPresenter presenter)
        {
            Invoke(() =>
                {
                    foreach (ListViewItem item in PresenterListView.Items)
                    {
                        if (item.Tag == null)
                        {
                            continue;
                        }

                        var tag = item.Tag as ItemTag;

                        if (tag == null || tag.Presenter != presenter)
                        {
                            continue;
                        }

                        item.Selected = true;

                        Presenter.PresenterSelected(tag.Presenter);
                    }
                });
        }

        public void ShowNavigationItems(IEnumerable<INavigationItem> navigationItems)
        {
            Invoke(() =>
                {
                    ContextToolStrip.Items.Clear();

                    var closeItem = ContextToolStrip.Items.Add("Close", Resources.Image_Exit);

                    closeItem.Click += CloseButton_Click;
                    closeItem.TextImageRelation = TextImageRelation.TextBeforeImage;

                    foreach (var navigationItem in navigationItems)
                    {
                        var stripItem = ContextToolStrip.Items.Add(navigationItem.Text, navigationItem.Image);

                        stripItem.Tag = navigationItem;
                        stripItem.TextImageRelation = TextImageRelation.TextBeforeImage;
                        stripItem.Enabled = Presenter.WorkItem.WorkItemPresenter.IsMessageEnabled(navigationItem.Message);

                        stripItem.Click += (ContextItemClicked);
                    }
                });
        }

        private void HookChangeEvents(Control control)
        {
            Invoke(() =>
                {
                    foreach (Control c in control.Controls)
                    {
                        c.TextChanged += ControlChanged;

                        var lc = c as ListControl;

                        if (lc != null)
                        {
                            lc.SelectedValueChanged += ControlChanged;
                        }

                        HookChangeEvents(c);
                    }
                });
        }

        private void ControlChanged(object sender, EventArgs e)
        {
            HasChanges = true;
        }

        private void PresenterImageChanged(object sender, PresenterImageChangedArgs args)
        {
            Invoke(() =>
                {
                    var key = ((IPresenter)sender).Text;

                    PresenterImageList.Images.RemoveByKey(key);
                    PresenterImageList.Images.Add(key, args.Image);

                });
        }

        private void PresenterTextChanged(object sender, PresenterTextChangedArgs args)
        {
            Invoke(() =>
                {
                    var items = GetItemByPresenterText(args.From);

                    if (items.Length == 0)
                    {
                        return;
                    }

                    items[0].Text = args.To;
                    items[0].ImageKey = args.To;

                    ChangeImageKey(args.From, args.To);

                });
        }

        private void ChangeImageKey(string from, string to)
        {
            Invoke(() =>
                {
                    var image = PresenterImageList.Images[from];

                    if (image == null)
                    {
                        return;
                    }

                    PresenterImageList.Images.RemoveByKey(from);

                    PresenterImageList.Images.Add(to, image);

                });
        }

        private ListViewItem[] GetItemByPresenterText(string text)
        {
            ListViewItem[] result = null;

            Invoke(() =>
                {
                    result = PresenterListView.Items.Find(text, false);

                });

            return result;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Invoke(() => Presenter.Close());
        }

        private void ContextItemClicked(object sender, EventArgs e)
        {
            var item = sender as ToolStripItem;

            if (item == null)
            {
                return;
            }

            var navigationItem = item.Tag as INavigationItem;

            if (navigationItem == null)
            {
                return;
            }

            Invoke(() => Presenter.InvokeMessage(navigationItem.Message));
        }

        private void PresenterListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            Invoke(() =>
                {
                    if (!HasSelectedPresenter)
                    {
                        Presenter.NoPresenterSelected();

                        return;
                    }

                    SelectPresenter();

                });
        }

        private void SelectPresenter()
        {
            Invoke(() =>
                {
                    foreach (ListViewItem item in PresenterListView.Items)
                    {
                        if (item.Tag == null)
                        {
                            continue;
                        }

                        var itemTag = (ItemTag)item.Tag;

                        var control = itemTag.ViewControl;

                        if (!item.Selected)
                        {
                            control.Hide();
                        }
                        else
                        {
                            control.Show();

                            Presenter.PresenterSelected(itemTag.Presenter);
                        }
                    }

                });
        }

        private void ContextToolbarView_Resize(object sender, EventArgs e)
        {
            PresenterColumnHeader.Width = PresenterListView.ClientSize.Width;
        }

        private class ItemTag
        {
            public ItemTag(IPresenter presenter, Control viewControl)
            {
                Presenter = presenter;
                ViewControl = viewControl;
            }

            public IPresenter Presenter { get; private set; }
            public Control ViewControl { get; private set; }
        }
    }

    public class GenericContextToolbarView : View<IContextToolbarPresenter>
    {
    }
}
