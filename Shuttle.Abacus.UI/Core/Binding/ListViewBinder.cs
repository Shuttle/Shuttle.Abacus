using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Shuttle.Abacus.Shell.Core.Binding
{
    public class ListViewBinder : IBinder<ListView>
    {
        private static readonly Type Fortype = typeof(ListView);

        public void Bind(string keyColumnName, IEnumerable<DataRow> rows, ListView to)
        {
            Bind(keyColumnName, rows, to, new List<string>(), new List<string>());
        }

        public Type ForType => Fortype;

        public void Bind(string keyColumnName, IEnumerable<DataRow> rows, ListView to,
            IEnumerable<string> visibleColumns, IEnumerable<string> hiddenColumns)
        {
            to.Columns.Clear();
            to.Items.Clear();

            var enumerableRows = rows as IList<DataRow> ?? rows.ToList();

            if (!enumerableRows.Any())
            {
                return;
            }

            var enumerableVisibleColumns = visibleColumns as IList<string> ?? visibleColumns.ToList();
            var enumerableHiddenColumns = hiddenColumns as IList<string> ?? hiddenColumns.ToList();

            BuildColumns(keyColumnName, enumerableRows.First().Table.Columns, to, enumerableVisibleColumns,
                enumerableHiddenColumns);

            foreach (var row in enumerableRows)
            {
                var item = to.Items.Add(string.Empty);

                var textSet = false;

                foreach (DataColumn column in row.Table.Columns)
                {
                    if (column.ColumnName.Equals(keyColumnName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        item.Name = Convert.ToString(row[keyColumnName]);

                        continue;
                    }

                    if (!IsColumnVisible(column, enumerableVisibleColumns, enumerableHiddenColumns))
                    {
                        continue;
                    }

                    var value = FormattedValue(column, row[column.ColumnName]);

                    if (textSet)
                    {
                        item.SubItems.Add(value);
                    }
                    else
                    {
                        item.Text = value;

                        textSet = true;
                    }
                }
            }
        }

        private static void BuildColumns(string keyColumnName, DataColumnCollection columns, ListView to,
            IEnumerable<string> visibleColumns, IEnumerable<string> hiddenColumns)
        {
            foreach (DataColumn column in columns)
            {
                if (column.ColumnName.Equals(keyColumnName, StringComparison.InvariantCultureIgnoreCase)
                    ||
                    !IsColumnVisible(column, visibleColumns, hiddenColumns))
                {
                    continue;
                }

                AddHeader(column, to);
            }
        }

        private static bool IsColumnVisible(DataColumn column, IEnumerable<string> visibleColumns,
            IEnumerable<string> hiddenColumns)
        {
            return
                visibleColumns.Any()
                &&
                visibleColumns.Contains(column.ColumnName)
                ||
                (!hiddenColumns.Any() || !hiddenColumns.Contains(column.ColumnName))
                &&
                !visibleColumns.Any()
                &&
                !hiddenColumns.Any();
        }

        private static string FormattedValue(DataColumn column, object value)
        {
            switch (column.DataType.Name)
            {
                case "Date":
                case "DateTime":
                {
                    return Convert.ToString(value, CultureInfo.CurrentUICulture.DateTimeFormat);
                }
                case "Byte":
                case "Decimal":
                case "Double":
                case "Int16":
                case "Int32":
                case "Int64":
                case "SByte":
                case "Single":
                case "UInt16":
                case "UInt32":
                case "UInt64":
                case "VarNumeric":
                case "Currency":
                {
                    return Convert.ToString(value, CultureInfo.CurrentUICulture.NumberFormat);
                }
            }

            return Convert.ToString(value);
        }

        private static void AddHeader(DataColumn column, ListView to)
        {
            var header = to.Columns.Add(column.ColumnName, column.Text());

            switch (column.DataType.Name)
            {
                case "Byte":
                case "Currency":
                case "Date":
                case "DateTime":
                case "Decimal":
                case "Double":
                case "Int16":
                case "Int32":
                case "Int64":
                case "SByte":
                case "Single":
                case "UInt16":
                case "UInt32":
                case "UInt64":
                case "VarNumeric":
                {
                    header.TextAlign = HorizontalAlignment.Right;

                    break;
                }
            }

            var width = column.MaxLength > 0
                ? column.MaxLength * 3
                : 120;

            if (width > 200)
            {
                width = 200;
            }

            header.Width = width;
        }
    }
}