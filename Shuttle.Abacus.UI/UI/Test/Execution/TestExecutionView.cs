using System;
using System.Globalization;
using System.Windows.Forms;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Messages.Test;
using Shuttle.Abacus.Shell.Models;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.UI.Test.Execution
{
    public partial class TestExecutionExecutionView : GenericTestExecutionView, ITestExecutionView
    {
        private readonly IConstraintComparison _constraintComparison;

        public TestExecutionExecutionView(IConstraintComparison constraintComparison)
        {
            Guard.AgainstNull(constraintComparison, "constraintComparison");

            _constraintComparison = constraintComparison;

            InitializeComponent();

            ImageList.Images.Add("Success", Resources.Image_Mark);
            ImageList.Images.Add("Error", Resources.Image_Error);
            ImageList.Images.Add("Waiting", Resources.Image_Hourglass);
        }

        public void AddTest(TestExecutionItemModel item)
        {
            var lvi = FindListViewItem(item.Id);

            if (lvi == null)
            {
                lvi = TestListView.Items.Add(item.Name, "Waiting");

                lvi.Name = item.Id.ToString();

                lvi.SubItems.Add("Executing");
                lvi.SubItems.Add($"{item.Comparison} {item.ExpectedResult}");

                lvi.Tag = item;

                Presenter.RequestExecution(item);
            }
        }

        public void TestExecuted(TestExecutedMessage message)
        {
            Guard.AgainstNull(message, "message");

            Invoke(() =>
            {
                var lvi = FindListViewItem(message.Id);

                if (lvi == null)
                {
                    return;
                }

                var item = (TestExecutionItemModel) lvi.Tag;

                lvi.SubItems[1].Text = "Complete";

                lvi.ImageKey = _constraintComparison
                    .IsSatisfiedBy(
                        item.ExpectedResultType,
                        message.Result.ToString(CultureInfo.InvariantCulture),
                        item.Comparison,
                        item.ExpectedResult)
                    ? "Success"
                    : "Error";
            });
        }

        private ListViewItem FindListViewItem(Guid id)
        {
            foreach (ListViewItem lvi in TestListView.Items)
            {
                if (lvi.Name.Equals(id.ToString()))
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