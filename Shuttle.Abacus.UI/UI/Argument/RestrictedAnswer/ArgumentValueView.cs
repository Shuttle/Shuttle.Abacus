using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Shuttle.Abacus.DTO;
using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Argument.RestrictedAnswer
{
    public partial class ArgumentValueView : GenericArgumentRestrictedAnswerView, IArgumentValueView
    {
        public ArgumentValueView()
        {
            InitializeComponent();
        }

        public bool HasSelectedItem
        {
            get { return AnswerListView.SelectedItems.Count > 0; }
        }

        public string AnswerValue
        {
            get { return Answer.Text; }
            set { Answer.Text = value; }
        }

        public bool HasValue()
        {
            return Answer.Text.Length > 0;
        }

        public void ShowValueError(string message)
        {
            SetError(Answer, message);
        }

        public List<string> Values
        {
            get
            {
                var result = new List<string>();

                foreach (ListViewItem item in AnswerListView.Items)
                {
                    result.Add(item.Text);
                }

                return result;
            }
            set
            {
                foreach (var item in value)
                {
                    AnswerListView.Items.Add(item);
                }
            }
        }

        public IRuleCollection<object> ArgumentValueRules
        {
            set
            {
                ViewValidator.Control(Answer).ShouldSatisfy(value);
            }
        }

        public ListViewItem AddValue(string answer)
        {
            var item = new ListViewItem(answer);

            AnswerListView.Items.Add(item);

            return item;
        }

        public void RemoveSelectedItem()
        {
            var item = SelectedItem();

            if (item == null)
            {
                return;
            }

            AnswerListView.Items.Remove(item);
        }

        private static void PopulateItem(ListViewItem item, string answer)
        {
            item.Text = answer;
        }

        public bool HasMoreThanOneItem()
        {
            return (AnswerListView.Items.Count > 1);
        }

        private ListViewItem SelectedItem()
        {
            return !HasSelectedItem
                       ? null
                       : AnswerListView.SelectedItems[0];
        }

        private void RemoveAnswer_Click(object sender, EventArgs e)
        {
            RemoveSelectedItem();
        }

        private void Answer_TextChanged(object sender, EventArgs e)
        {
            ShowValueError(string.Empty);
        }

        private void AnswerListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = SelectedItem();

            if (item == null)
            {
                return;
            }

            Answer.Text = item.Text;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (!Presenter.IsValueOK())
            {
                return;
            }

            AddValue(AnswerValue);
        }
    }

    public class GenericArgumentRestrictedAnswerView : View<IArgumentValuePresenter>
    {
    }
}
