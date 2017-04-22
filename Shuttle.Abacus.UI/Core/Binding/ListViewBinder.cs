using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace Shuttle.Abacus.UI.Core.Binding
{
    public class ListViewBinder : IBinder<ListView>
    {
        private static readonly Type fortype = typeof (ListView);

        public void Bind(IQueryResult result, ListView to)
        {
            Bind(result, to, new List<QueryColumn>(), new List<QueryColumn>());
        }

        public void Bind(IQueryResult result, ListView to, IList<QueryColumn> visibleColumns,
                         IList<QueryColumn> hiddenColumns)
        {
            to.Columns.Clear();
            to.Items.Clear();

            var keyColumn = GetIdentifier(result);

            BuildColumns(result, to, visibleColumns, hiddenColumns);

            foreach (DataRow row in result.Table.Rows)
            {
                var item = to.Items.Add(string.Empty);

                item.Name = Convert.ToString(row[keyColumn]);

                var textSet = false;

                foreach (var column in result.Columns)
                {
                    if (column.IsIdentifier)
                    {
                        continue;
                    }

                    if (!IsColumnVisible(column, result, visibleColumns, hiddenColumns))
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

        public Type ForType
        {
            get { return fortype; }
        }

        private static int GetIdentifier(IQueryResult result)
        {
            var i = 0;

            foreach (var column in result.Columns)
            {
                if (column.IsIdentifier)
                {
                    break;
                }
                i++;
            }
            return i;
        }

        private static void BuildColumns(IQueryResult result, ListView to, ICollection<QueryColumn> visibleColumns,
                                         ICollection<QueryColumn> hiddenColumns)
        {
            foreach (var column in result.Columns)
            {
                if (!column.IsIdentifier && IsColumnVisible(column, result, visibleColumns, hiddenColumns))
                {
                    AddHeader(column, to);
                }
            }
        }

        private static bool IsColumnVisible(QueryColumn column, IQueryResult result,
                                            ICollection<QueryColumn> visibleColumns,
                                            ICollection<QueryColumn> hiddenColumns)
        {
            return
                result.Columns.Contains(column)
                &&
                (
                    visibleColumns.Count > 0
                    && visibleColumns.Contains(column)
                    || (hiddenColumns.Count <= 0 || !hiddenColumns.Contains(column))
                       && (visibleColumns.Count == 0 && hiddenColumns.Count == 0)
                );
        }

        private static string FormattedValue(QueryColumn column, object value)
        {
            switch (column.DbType)
            {
                case DbType.Date:
                case DbType.DateTime:
                    {
                        return Convert.ToString(value, CultureInfo.CurrentUICulture.DateTimeFormat);
                    }
                case DbType.Byte:
                case DbType.Decimal:
                case DbType.Double:
                case DbType.Int16:
                case DbType.Int32:
                case DbType.Int64:
                case DbType.SByte:
                case DbType.Single:
                case DbType.UInt16:
                case DbType.UInt32:
                case DbType.UInt64:
                case DbType.VarNumeric:
                case DbType.Currency:
                    {
                        return Convert.ToString(value, CultureInfo.CurrentUICulture.NumberFormat);
                    }
            }

            return Convert.ToString(value);
        }

        private static void AddHeader(QueryColumn column, ListView to)
        {
            var header = to.Columns.Add(column.ColumnName, column.Text);

            switch (column.DbType)
            {
                case DbType.Byte:
                case DbType.Currency:
                case DbType.Date:
                case DbType.DateTime:
                case DbType.Decimal:
                case DbType.Double:
                case DbType.Int16:
                case DbType.Int32:
                case DbType.Int64:
                case DbType.SByte:
                case DbType.Single:
                case DbType.UInt16:
                case DbType.UInt32:
                case DbType.UInt64:
                case DbType.VarNumeric:
                    {
                        header.TextAlign = HorizontalAlignment.Right;

                        break;
                    }
            }

            var width = column.Size.HasValue
                            ? column.Size.Value*3
                            : 120;

            if (width > 200)
            {
                width = 200;
            }

            header.Width = width;
        }
    }
}
