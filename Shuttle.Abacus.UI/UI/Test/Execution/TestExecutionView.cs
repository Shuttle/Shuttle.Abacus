using System;
using System.Windows.Forms;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Models;

namespace Shuttle.Abacus.Shell.UI.Test.Execution
{
    public partial class TestExecutionExecutionView : GenericTestExecutionView, ITestExecutionView
    {
        public TestExecutionExecutionView()
        {
            InitializeComponent();
        }

        public void AddTest(TestExecutionItemModel item)
        {
            var lvi = FindListViewItem(item);

            if (lvi == null)
            {
                lvi = TestListView.Items.Add(item.Name);

                lvi.Name = item.Id.ToString();

                lvi.SubItems.Add("Executing");
                lvi.SubItems.Add($"{item.Comparison} {item.ExpectedResult}");

                lvi.Tag = item;

                Presenter.RequestExecution(item);
            }
        }

        private ListViewItem FindListViewItem(TestExecutionItemModel item)
        {
            foreach (ListViewItem lvi in TestListView.Items)
            {
                if (lvi.Name.Equals(item.Id.ToString()))
                {
                    return lvi;
                }
            }

            return null;
        }
    }

    public class GenericTestExecutionView : View<ITestExecutionPresenter>
    {
    }
}