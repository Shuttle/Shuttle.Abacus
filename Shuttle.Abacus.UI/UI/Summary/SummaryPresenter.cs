using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Core;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Summary
{
    public class SummaryPresenter : Presenter<ISummaryView>, ISummaryPresenter
    {
        private readonly object[] emptyValue = { "(empty)" };

        public SummaryPresenter(ISummaryView view)
            : base(view)
        {
            Text = "Summary View";
            Image = Resources.Image_Show;
        }

        public void Populate(IEnumerable<NamedQueryResult> namedQueryResults)
        {
            View.Clear();

            if (!namedQueryResults.Any())
            {
                return;
            }

            var count = LongestColumnsCount(namedQueryResults);

            if (count == 0)
            {
                return;
            }

            View.EnsureColumnCount(count);

            foreach (var namedQueryResult in namedQueryResults)
            {
                View.AddGroup(namedQueryResult.Name);

                var firstRow = namedQueryResult.Rows.FirstOrDefault();

                if (firstRow == null)
                {
                    continue;
                }

                var columns = firstRow.Table.Columns;

                switch (namedQueryResult.Type)
                {
                    case NamedQueryResult.DisplayType.Table:
                        {
                            View.AddRow();

                            foreach (DataColumn column in columns)
                            {
                                    View.AddHeading(column.Text());
                            }

                            if (namedQueryResult.Rows.Any())
                            {
                                foreach (DataRow row in namedQueryResult.Rows)
                                {
                                    View.AddRow(RowValues(namedQueryResult.Rows, row));
                                }
                            }
                            else
                            {
                                View.AddRow(emptyValue);
                            }

                            break;
                        }
                        case NamedQueryResult.DisplayType.Row:
                        {
                            var columnIndex = 0;
                            var values = RowValues(namedQueryResult.Rows, firstRow);

                            foreach (DataColumn column in columns)
                            {
                                View.AddAttribute(column.Text(), values[columnIndex]);

                                columnIndex++;
                            }

                            break;
                        }
                }

                View.AddRow();
            }
        }

        private static object[] RowValues(IEnumerable<DataRow> queryResult, DataRow row)
        {
            //if (queryResult.HasIdentifierColumn())
            //{
            //    var length = row.ItemArray.Length - 1;

            //    var values = Array.CreateInstance(typeof (object), length);

            //    Array.Copy(row.ItemArray, 1, values, 0, length);

            //    return (object[]) values;
            //}

            return row.ItemArray;
        }

        private static int LongestColumnsCount(IEnumerable<NamedQueryResult> namedQueryResults)
        {
            var i = 0;

            foreach (var namedQueryResult in namedQueryResults)
            {
                var count = namedQueryResult.Type == NamedQueryResult.DisplayType.Table
                                ? namedQueryResult.Rows.GetRow().Table.Columns.Count
                                : 2;

                if (count > i)
                {
                    i = count;
                }
            }

            return i;
        }
    }
}
