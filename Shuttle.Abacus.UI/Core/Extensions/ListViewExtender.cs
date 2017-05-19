using System.Windows.Forms;

namespace Shuttle.Abacus.Shell.Core.Extensions
{
    public class ListViewExtender
    {
        private readonly ListView lvw;

        public ListViewExtender(ListView lvw)
        {
            this.lvw = lvw;
        }

        public ListViewItem SelectedItem()
        {
            return !HasSelectedItem
                       ? null
                       : lvw.SelectedItems[0];
        }

        public bool HasSelectedItem => lvw.SelectedItems.Count > 0;

        public bool IsFirstItem(ListViewItem item)
        {
            return item.Index == 0;
        }

        public bool IsLastItem(ListViewItem item)
        {
            return lvw.Items.Count - 1 == item.Index;
        }

        public bool HasMoreThanOneItem => lvw.Items.Count > 1;

        public void MoveItemUp(ListViewItem item)
        {
            var index = item.Index - 1;

            lvw.Items.Remove(item);
            lvw.Items.Insert(index, item);
        }

        public void MoveItemDown(ListViewItem item)
        {
            var index = item.Index + 1;

            lvw.Items.Remove(item);
            lvw.Items.Insert(index, item);
        }

        public void MoveSelectedUp()
        {
            var item = SelectedItem();

            if (!HasSelectedItem || !HasMoreThanOneItem || IsFirstItem(item))
            {
                return;
            }

            MoveItemUp(item);
        }

        public void MoveSelectedDown()
        {
            var item = SelectedItem();

            if (!HasSelectedItem || !HasMoreThanOneItem || IsLastItem(item))
            {
                return;
            }

            MoveItemDown(item);
        }

        public void RemoveSelectedItem()
        {
            var item = SelectedItem();

            if (item == null)
            {
                return;
            }

            lvw.Items.Remove(item);
        }
    }
}
