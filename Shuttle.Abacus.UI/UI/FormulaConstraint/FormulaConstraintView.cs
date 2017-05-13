﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Extensions;
using Shuttle.Abacus.UI.Core.Formatters;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;

namespace Shuttle.Abacus.UI.UI.FormulaConstraint
{
    public partial class FormulaConstraintView : GenericConstraintView, IFormulaConstraintView
    {
        private readonly ListViewExtender _listViewExtender ;
        private MoneyFormatter _valueFormatter;
        private IEnumerable<ConstraintTypeModel> _constraintTypes;

        public FormulaConstraintView()
        {
            InitializeComponent();

            _listViewExtender = new ListViewExtender(ConstraintsListView);
        }

        public void PopulateArguments(IEnumerable<ArgumentModel> items)
        {
            Answer.Items.Clear();

            Argument.DisplayMember = "ConstraintName";
            items.ForEach(item => Argument.Items.Add(item));
        }

        public void SetContraintTypes(IEnumerable<ConstraintTypeModel> list)
        {
            _constraintTypes = list;
        }

        public void PopulateConstraintTypes(bool restricted)
        {
            Constraint.Items.Clear();
            Constraint.DisplayMember = "Text";

            _constraintTypes.ForEach(model =>
                {
                    if (!restricted || model.EnabledForRestrictedAnswers)
                    {
                        Constraint.Items.Add(model);
                    }
                });
        }

        public ArgumentModel ArgumentModel => Argument.SelectedItem as ArgumentModel;

        public ConstraintTypeModel ConstraintTypeModel => Constraint.SelectedItem as ConstraintTypeModel;

        public void EnableAnswerSelection()
        {
            Answer.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void EnableAnswerEntry()
        {
            Answer.DropDownStyle = ComboBoxStyle.DropDown;
            Answer.Text = string.Empty;
            Answer.Items.Clear();
        }

        public void PopulateAnswers(IEnumerable<DataRow> rows)
        {
            Answer.Items.Clear();

            rows.ForEach(row =>
                {
                    var answer = ArgumentColumns.ValueColumns.Value.MapFrom(row);

                    if (!ContainsAnswerName(answer))
                    {
                        Answer.Items.Add(answer);
                    }
                });
        }

        private bool ContainsAnswerName(string name)
        {
            foreach (string item in Answer.Items)
            {
                if (name.Equals(item, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public string AnswerValue
        {
            get { return Answer.Text; }
            set { Answer.Text = value; }
        }

        public bool HasAnswer => Answer.Text.Length > 0;

        public bool HasAnswers => Answer.Items.Count > 0;

        public bool HasArgument => Argument.Text.Length > 0;

        public bool HasConstraint => Constraint.Text.Length > 0;

        public List<Domain.FormulaConstraint> Constraints
        {
            get
            {
                var result = new List<Domain.FormulaConstraint>();
                var sequenceNumber = 1;

                foreach (ListViewItem item in ConstraintsListView.Items)
                {
                    var tag = (ItemTag) item.Tag;

                    result.Add(new Domain.FormulaConstraint(
                        sequenceNumber,
                        tag.ArgumentName,
                        tag.ConstraintName,
                        tag.ValueSelection));
                }

                return result;
            }
            set { value.ForEach(constraint => AddConstraint(constraint.ArgumentName, constraint.ComparisonType, constraint.Value)); }
        }

        private string GetArgumentName(Guid argumentId)
        {
            foreach (DataRow row in Answer.Items)
            {
                if (ArgumentColumns.Id.MapFrom(row).Equals(argumentId))
                {
                    return ArgumentColumns.Name.MapFrom(row);
                }
            }

            return "(not found)";
        }

        public ComboBox ValueSelectionControl => Answer;

        public TextBox FormattedControl => FormattedTextBox;

        public void ShowAnswerError(string message)
        {
            SetError(Answer, message);
        }

        public void ShowArgumentError()
        {
            SetError(Argument, "Please select the argument to use.");
        }

        public void ShowConstraintError()
        {
            SetError(Constraint, "Please select the constraint to use.");
        }

        public void AddConstraint(string argumentName, string comparisonType, string value)
        {
            var item = new ListViewItem();

            item.SubItems.Add(string.Empty);
            item.SubItems.Add(string.Empty);

            ConstraintsListView.Items.Add(PopulateItem(item, argumentName, comparisonType, value));
        }

        public void ShowAllConstraints()
        {
            PopulateConstraintTypes(false);
        }

        public void ShowAnswerCatalogConstraints()
        {
            PopulateConstraintTypes(true);
        }

        public void AttachValueFormatter(MoneyFormatter formatter)
        {
            _valueFormatter = formatter;
        }

        public void DetachValueFormatter()
        {
            FormattedTextBox.Text = string.Empty;

            if (_valueFormatter == null)
            {
                return;
            }

            _valueFormatter.Dispose();

            _valueFormatter = null;
        }

        private void ArgumentName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearError(Argument);

            Presenter.ArgumentChanged();
        }

        private void Constraint_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetError(Constraint, string.Empty);
        }

        private void ValueSelection_TextChanged(object sender, EventArgs e)
        {
            SetError(Answer, string.Empty);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (Presenter.ConstraintOK())
            {
                AddConstraint(ArgumentModel.Name, ConstraintTypeModel.Name, AnswerValue);
            }
        }

        private static ListViewItem PopulateItem(ListViewItem item, string argumentName, string comparisonType, string value)
        {
            item.Text = argumentName;
            item.SubItems[1].Text = comparisonType;
            item.SubItems[2].Text = value;

            item.Tag = new ItemTag(argumentName, comparisonType, value);

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

            PopulateItem(_listViewExtender.SelectedItem(), ArgumentModel.Name, ConstraintTypeModel.Name, AnswerValue);
        }

        private class ItemTag
        {
            public ItemTag(string argumentName, string comparisonType, string value)
            {
                ArgumentName = argumentName;
                ConstraintName = comparisonType;
                ValueSelection = value;
            }

            public string ArgumentName { get; private set; }
            public string ConstraintName { get; private set; }
            public string ValueSelection { get; private set; }
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