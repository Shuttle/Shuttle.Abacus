using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace Shuttle.Abacus.UI.Models
{
    public class SimpleListModel
    {
        private readonly List<string> _visibleColumns = new List<string>();
        private readonly List<string> _hiddenColumns = new List<string>();
        private bool _hasCheckBoxes;

        public SimpleListModel(string keyColumnName, DataRow row) 
            : this(keyColumnName, new[] { row })
        {
        }

        public SimpleListModel(string keyColumnName, IEnumerable<DataRow> rows)
        {
            KeyColumnName = keyColumnName;
            Rows = rows;
        }

        public string KeyColumnName { get; }
        public IEnumerable<DataRow> Rows { get; }

        public IEnumerable<string> VisibleColumns => new ReadOnlyCollection<string>( _visibleColumns);
        public IEnumerable<string> HiddenColumns => new ReadOnlyCollection<string>( _hiddenColumns);
        public bool HasCheckBoxes => _hasCheckBoxes;

        public SimpleListModel WithCheckBoxes()
        {
            _hasCheckBoxes = true;

            return this;
        }

        public SimpleListModel AddHiddenColumn(string columnName)
        {
            _hiddenColumns.Add(columnName);

            return this;
        }

        public SimpleListModel AddVisibleColumn(string columnName)
        {
            _visibleColumns.Add(columnName);

            return this;
        }
    }
}
