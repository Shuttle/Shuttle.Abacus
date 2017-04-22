using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Shuttle.Abacus.DTO;
using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Argument.RestrictedAnswer
{
    public partial class ArgumentRestrictedAnswerView : GenericArgumentRestrictedAnswerView, IArgumentRestrictedAnswerView
    {
        public ArgumentRestrictedAnswerView()
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

        public bool HasAnswer()
        {
            return Answer.Text.Length > 0;
        }

        public void ShowAnswerError(string message)
        {
            SetError(Answer, message);
        }

        public List<ArgumentRestrictedAnswerDTO> ArgumentAnswerCatalog
        {
            get
            {
                var result = new List<ArgumentRestrictedAnswerDTO>();

                foreach (ListViewItem item in AnswerListView.Items)
                {
                    result.Add(new ArgumentRestrictedAnswerDTO(item.Text));
                }

                return result;
            }
            set
            {
                foreach (var dto in value)
                {
                    AnswerListView.Items.Add(dto.Answer);
                }
            }
        }

        public IRuleCollection<object> ArgumentRestrictedAnswerRules
        {
            set
            {
                ViewValidator.Control(Answer).ShouldSatisfy(value);
            }
        }

        public ListViewItem AddRestrictedAnswer(string answer)
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
            ShowAnswerError(string.Empty);
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
            if (!Presenter.AnswerOK())
            {
                return;
            }

            AddRestrictedAnswer(AnswerValue);
        }
    }

    public class GenericArgumentRestrictedAnswerView : View<IArgumentRestrictedAnswerPresenter>
    {
    }
}
