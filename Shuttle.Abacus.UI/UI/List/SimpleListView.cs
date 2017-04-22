using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;

namespace Shuttle.Abacus.UI.UI.List
{
    public partial class SimpleListView : GenericSimpleListView, ISimpleListView
    {
        public SimpleListView()
        {
            InitializeComponent();
        }

        public void PopulateListView(SimpleListModel model)
        {
            Invoke(
                () =>
                BinderProvider.GetBinderFor<ListView>().Bind(model.Rows, ListView, model.VisibleColumns,
                                                             model.HiddenColumns));
        }

        public Guid SelectedId
        {
            get
            {
                return !HasSelectedItem
                           ? Guid.Empty
                           : new Guid(SelectedItem().Name);
            }
        }

        public string SelectedText
        {
            get
            {
                return !HasSelectedItem
                           ? string.Empty
                           : SelectedItem().Text;
            }
        }

        public bool HasCheckedItems
        {
            get { return CheckedItems.Count() > 0; }
        }

        public IEnumerable<ListViewItem> CheckedItems
        {
            get
            {
                var result = new List<ListViewItem>();

                foreach (ListViewItem item in ListView.Items)
                {
                    if (item.Checked)
                    {
                        result.Add(item);
                    }
                }

                return result;
            }
        }

        public IEnumerable<ListViewItem> Items
        {
            get
            {
                var result = new List<ListViewItem>();

                foreach (ListViewItem item in ListView.Items)
                {
                    result.Add(item);
                }

                return result;
            }
        }

        public void AssignContextMenuStrip(ContextMenuStrip strip)
        {
            ListView.ContextMenuStrip = strip;
        }

        public ListViewItem SelectedItem()
        {
            return ListView.SelectedItems[0];
        }

        public bool HasSelectedItem
        {
            get { return ListView.SelectedItems.Count > 0; }
        }

        public void ShowCheckboxes()
        {
            ListView.CheckBoxes = true;
        }

        public void MarkAll()
        {
            foreach (ListViewItem item in ListView.Items)
            {
                item.Checked = true;
            }
        }

        public void InvertMarks()
        {
            foreach (ListViewItem item in ListView.Items)
            {
                item.Checked = !item.Checked;
            }
        }

        public void MoveUp(ListViewItem item)
        {
            var position = ListView.Items.IndexOf(item);

            if (position == 0)
            {
                return;
            }

            ListView.Items.Remove(item);

            ListView.Items.Insert(position - 1, item);
        }

        public void MoveDown(ListViewItem item)
        {
            var position = ListView.Items.IndexOf(item);

            if (position == ListView.Items.Count - 1)
            {
                return;
            }

            ListView.Items.Remove(item);

            ListView.Items.Insert(position + 1, item);
        }

        public bool Contains(Guid id)
        {
            var result = false;

            Invoke(() =>
                {
                    foreach (ListViewItem item in ListView.Items)
                    {
                        if (id.Equals(new Guid(item.Name)))
                        {
                            result = true;
                        }
                    }
                });

            return result;
        }

        public bool FullRowSelect
        {
            get { return ListView.FullRowSelect; }
            set { ListView.FullRowSelect = value; }
        }

        public bool HasElectedItems
        {
            get { return HasCheckedItems || HasSelectedItem; }
        }

        public IEnumerable<ListViewItem> ElectedItems
        {
            get
            {
                var result = new List<ListViewItem>(CheckedItems);

                if (result.Count == 0 && HasSelectedItem)
                {
                    foreach (ListViewItem item in ListView.Items)
                    {
                        if (item.Selected)
                        {
                            result.Add(item);
                        }
                    }
                }

                return result;
            }
        }

        private void UserListView_DoubleClick(object sender, EventArgs e)
        {
            if (!HasSelectedItem)
            {
                return;
            }

            Presenter.DoubleClick();
        }
    }

    public class GenericSimpleListView : View<ISimpleListPresenter>
    {
    }
}
