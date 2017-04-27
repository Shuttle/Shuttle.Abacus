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
        private readonly object[] emptyValue = {"(empty)"};

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
                            foreach (var row in namedQueryResult.Rows)
                            {
                                View.AddRow(row.ItemArray);
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

                        foreach (DataColumn column in columns)
                        {
                            View.AddAttribute(column.Text(), firstRow[columnIndex]);

                            columnIndex++;
                        }

                        break;
                    }
                }

                View.AddRow();
            }
        }

        private static int LongestColumnsCount(IEnumerable<NamedQueryResult> namedQueryResults)
        {
            var i = 0;

            foreach (var namedQueryResult in namedQueryResults)
            {
                var count = namedQueryResult.Type == NamedQueryResult.DisplayType.Table
                    ? namedQueryResult.Rows.Any()
                        ? namedQueryResult.Rows.GetRow().Table.Columns.Count
                        : 2
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