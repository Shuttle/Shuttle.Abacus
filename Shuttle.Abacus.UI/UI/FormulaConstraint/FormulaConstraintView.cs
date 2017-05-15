using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Extensions;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;

namespace Shuttle.Abacus.UI.UI.FormulaConstraint
{
    public partial class FormulaConstraintView : GenericConstraintView, IFormulaConstraintView
    {
        private readonly ListViewExtender _listViewExtender;

        public FormulaConstraintView()
        {
            InitializeComponent();

            _listViewExtender = new ListViewExtender(ConstraintsListView);
        }

        public void PopulateArguments(IEnumerable<ArgumentModel> items)
        {
            ValueSelectionControl.Items.Clear();

            items.ForEach(item => Argument.Items.Add(item));
        }

        public ArgumentModel ArgumentModel => Argument.SelectedItem as ArgumentModel;

        public string ComparisonValue => (string) Constraint.SelectedItem;

        public void PopulateArgumentValues(IEnumerable<DataRow> rows)
        {
            rows.ForEach(row =>
            {
                var answer = ArgumentColumns.ValueColumns.Value.MapFrom(row);

                if (!ContainsAnswerName(answer))
                {
                    ValueSelectionControl.Items.Add(answer);
                }
            });
        }

        public string AnswerValue
        {
            get { return ValueSelectionControl.Text; }
            set { ValueSelectionControl.Text = value; }
        }

        public bool HasAnswer => ValueSelectionControl.Text.Length > 0;

        public bool HasArgument => Argument.Text.Length > 0;

        public bool HasConstraint => Constraint.Text.Length > 0;

        public IEnumerable<FormulaConstraintModel> FormulaConstraints
        {
            get
            {
                foreach (ListViewItem item in ConstraintsListView.Items)
                {
                    yield return (FormulaConstraintModel)item.Tag;
                }
            }
            set
            {
                value.ForEach(constraint => AddConstraint(constraint.ArgumentName, constraint.Comparison,
                    constraint.Value));
            }
        }

        public void ShowAnswerError(string message)
        {
            SetError(ValueSelectionControl, message);
        }

        public void ShowArgumentError()
        {
            SetError(Argument, "Please select the argument to use.");
        }

        public void ShowConstraintError()
        {
            SetError(Constraint, "Please select the constraint to use.");
        }

        public void AddConstraint(string argumentName, string comparison, string value)
        {
            var item = new ListViewItem();

            item.SubItems.Add(string.Empty);
            item.SubItems.Add(string.Empty);

            ConstraintsListView.Items.Add(PopulateItem(item, argumentName, comparison, value));
        }

        public void ShowAllConstraints()
        {
        }

        public ComboBox ValueSelectionControl { get; private set; }

        private bool ContainsAnswerName(string name)
        {
            foreach (string item in ValueSelectionControl.Items)
            {
                if (name.Equals(item, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private void ArgumentName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearError(Argument);

            ValueSelectionControl.Items.Clear();

            Presenter.PopulateArgumentValues();
        }

        private void Constraint_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetError(Constraint, string.Empty);
        }

        private void ValueSelection_TextChanged(object sender, EventArgs e)
        {
            SetError(ValueSelectionControl, string.Empty);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (Presenter.ConstraintOK())
            {
                AddConstraint(ArgumentModel.Name, ComparisonValue, AnswerValue);
            }
        }

        private static ListViewItem PopulateItem(ListViewItem item, string argumentName, string comparison,
            string value)
        {
            item.Text = argumentName;
            item.SubItems[1].Text = comparison;
            item.SubItems[2].Text = value;

            item.Tag = new FormulaConstraintModel
            {
                ArgumentName = argumentName,
                Comparison = comparison,
                Value = value
            };

            return item;
        }

        private void ConstraintsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = _listViewExtender.SelectedItem();

            var b = item != null;

            if (b)
            {
                Argument.Text = item.Text;
                Constraint.Text = item.SubItems[1].Text;
                AnswerValue = item.SubItems[2].Text;
            }

            MoveDownButton.Enabled = b;
            MoveUpButton.Enabled = b;
            RemoveButton.Enabled = b;
            ApplyButton.Enabled = b;
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            _listViewExtender.RemoveSelectedItem();
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            if (!_listViewExtender.HasSelectedItem)
            {
                return;
            }

            if (!Presenter.ConstraintOK())
            {
                return;
            }

            PopulateItem(_listViewExtender.SelectedItem(), ArgumentModel.Name, ComparisonValue, AnswerValue);
        }

        private void MoveUpButton_Click(object sender, EventArgs e)
        {
            _listViewExtender.MoveSelectedUp();
        }

        private void MoveDownButton_Click(object sender, EventArgs e)
        {
            _listViewExtender.MoveSelectedDown();
        }

        private void Answer_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }

    public class GenericConstraintView : View<IFormulaConstraintPresenter>
    {
    }
}