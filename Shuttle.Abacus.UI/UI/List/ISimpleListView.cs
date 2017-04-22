using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;

namespace Shuttle.Abacus.UI.UI.List
{
    public interface ISimpleListView : IView
    {
        void PopulateListView(SimpleListModel model);
        Guid SelectedId { get; }
        string SelectedText { get; }
        bool HasCheckedItems { get; }
        IEnumerable<ListViewItem> CheckedItems { get; }
        IEnumerable<ListViewItem> Items { get; }
        void AssignContextMenuStrip(ContextMenuStrip strip);
        ListViewItem SelectedItem();
        bool HasSelectedItem { get; }
        void ShowCheckboxes();
        void MarkAll();
        void InvertMarks();
        void MoveUp(ListViewItem item);
        void MoveDown(ListViewItem item);
        bool Contains(Guid id);
        bool FullRowSelect { get; set; }
        bool HasElectedItems { get; }
        IEnumerable<ListViewItem> ElectedItems { get; }
    }
}
