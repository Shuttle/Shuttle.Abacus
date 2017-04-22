using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Summary
{
    public class SummaryPresenter : Presenter<ISummaryView>, ISummaryPresenter
    {
        private readonly object[] emptyValue = new object[] { "(empty)" };

        public SummaryPresenter(ISummaryView view)
            : base(view)
        {
            Text = "Summary View";
            Image = Resources.Image_Show;
        }

        public void Populate(IEnumerable<NamedQueryResult> namedQueryResults)
        {
            View.Clear();

            if (namedQueryResults.Count() == 0)
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

                switch (namedQueryResult.Type)
                {
                    case NamedQueryResult.DisplayType.Table:
                        {
                            View.AddRow();

                            foreach (var queryColumn in namedQueryResult.QueryResult.Columns)
                            {
                                if (!queryColumn.IsIdentifier)
                                {
                                    View.AddHeading(queryColumn.Text);
                                }
                            }

                            if (namedQueryResult.QueryResult.Table.Rows.Count > 0)
                            {
                                foreach (DataRow row in namedQueryResult.QueryResult.Table.Rows)
                                {
                                    View.AddRow(RowValues(namedQueryResult.QueryResult, row));
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
                            var column = 0;
                            var values = RowValues(namedQueryResult.QueryResult, namedQueryResult.QueryResult.Row);

                            foreach (var queryColumn in namedQueryResult.QueryResult.Columns)
                            {
                                if (queryColumn.IsIdentifier)
                                {
                                    continue;
                                }

                                View.AddAttribute(queryColumn.Text, values[column]);

                                column++;
                            }

                            break;
                        }
                }

                View.AddRow();
            }
        }

        private static object[] RowValues(IEnumerable<DataRow> queryResult, DataRow row)
        {
            if (queryResult.HasIdentifierColumn())
            {
                var length = row.ItemArray.Length - 1;

                var values = Array.CreateInstance(typeof (object), length);

                Array.Copy(row.ItemArray, 1, values, 0, length);

                return (object[]) values;
            }

            return row.ItemArray;
        }

        private static int LongestColumnsCount(IEnumerable<NamedQueryResult> namedQueryResults)
        {
            var i = 0;

            foreach (var namedQueryResult in namedQueryResults)
            {
                var count = namedQueryResult.Type == NamedQueryResult.DisplayType.Table
                                ? namedQueryResult.QueryResult.Columns.Count()
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
