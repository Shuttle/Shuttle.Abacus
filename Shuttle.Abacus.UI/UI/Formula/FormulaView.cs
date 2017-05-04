﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.DTO;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;

namespace Shuttle.Abacus.UI.UI.Formula
{
    public partial class FormulaView : GenericFormulaView, IFormulaView
    {
        public FormulaView()
        {
            InitializeComponent();
        }

        protected bool HasMoreThanOneItem
        {
            get { return OperationsListView.Items.Count > 1; }
        }

        public string OperationValue
        {
            get { return Operation.Text; }
            set { Operation.Text = value; }
        }

        public bool HasOperation
        {
            get { return Operation.SelectedIndex > -1; }
        }

        public OperationTypeModel OperationTypeModel
        {
            get { return Operation.SelectedItem as OperationTypeModel; }
        }

        public bool HasSelectedItem
        {
            get { return OperationsListView.SelectedItems.Count > 0; }
        }

        public void ShowOperationTypeError()
        {
            SetError(Operation, "Please select the operation type.");
        }

        public void RemoveSelectedItem()
        {
            var item = SelectedItem();

            if (item == null)
            {
                return;
            }

            OperationsListView.Items.Remove(item);
        }

        public void DisableValues()
        {
            SetError(ValueSelection, string.Empty);

            ValueSelectionLabel.Text = "(not applicable)";
            ValueSelection.SelectedItem = null;
            ValueSelection.Text = string.Empty;
            ValueSelection.Enabled = false;
        }

        public List<FormulaOperation> FormulaOperations
        {
            get
            {
                var result = new List<FormulaOperation>();
                var sequenceNumber = 1;

                foreach (ListViewItem item in OperationsListView.Items)
                {
                    var tag = (ItemTag) item.Tag;

                    //TODO
                    //result.Add(new FormulaOperation(
                    //    sequenceNumber++,
                    //    tag.Operation,
                    //    tag.ValueSource,
                    //    tag.ValueSelection,
                    //    "???"
                    //));
                }

                return result;
            }
            set
            {
                //TODO
                //value.ForEach(dto => AddOperation(dto.OperationType, dto.ValueSourceType, dto.ValueSelection, dto.Text));
            }
        }

        public string ValueSourceValue
        {
            get { return ValueSource.Text; }
            set { ValueSource.Text = value; }
        }

        public ValueSourceTypeModel ValueSourceTypeModel
        {
            get { return ValueSource.SelectedItem as ValueSourceTypeModel; }
        }

        public string ValueSelectionValue
        {
            get
            {
                return ValueSourceTypeModel.IsSelection
                           ? ((SelectionItem) ValueSelection.SelectedItem).Id.ToString("n")
                           : ValueSelection.Text;
            }
            set
            {
                if (ValueSourceTypeModel.IsSelection)
                {
                    ValueSelection.SelectedItem = FindSelection(new Guid(value));
                }
                else
                {
                    ValueSelection.Text = value;
                }
            }
        }

        public string ValueSelectionText
        {
            get { return ValueSelection.Text; }
        }

        public bool HasValueSource
        {
            get { return ValueSource.SelectedIndex > -1; }
        }

        public bool HasValueSelection
        {
            get { return ValueSelection.Text.Length > 0; }
        }

        public void PopulateValueSources(IEnumerable<ValueSourceTypeModel> models)
        {
            ValueSource.DisplayMember = "Text";

            models.ForEach(source => ValueSource.Items.Add(source));
        }

        public void PopulateArguments(IEnumerable<ArgumentModel> arguments)
        {
            throw new NotImplementedException();
            arguments.ForEach(item =>
                {
                    //if (item.IsNumber)
                    //{
                    //    ValueSelection.Items.Add(new SelectionItem(item.Id, item.Name));
                    //}
                });
        }

        public void EnableValueSelection(string text)
        {
            ValueSelectionLabel.Text = text;

            ValueSelection.ValueMember = "Id";
            ValueSelection.DisplayMember = "Text";
            ValueSelection.Enabled = true;
            ValueSelection.DropDownStyle = ComboBoxStyle.DropDownList;
            ValueSelection.Items.Clear();
        }

        public void ShowValueSourceError()
        {
            SetError(ValueSource, "Please select the value source.");
        }

        public void ShowValueSelectionError(string message)
        {
            SetError(ValueSelection, message);
        }

        public void EnableValueEntry(string text)
        {
            ValueSelectionLabel.Text = text;

            ValueSelection.Enabled = true;
            ValueSelection.DropDownStyle = ComboBoxStyle.DropDown;
            ValueSelection.ValueMember = string.Empty;
            ValueSelection.DisplayMember = string.Empty;
            ValueSelection.Text = string.Empty;
        }

        public void PopulatePrecedingCalculations(IEnumerable<CalculationDTO> enumerable)
        {
            foreach (var dto in enumerable)
            {
                ValueSelection.Items.Add(new SelectionItem(dto.CalculationId, dto.Name));
            }
        }

        public void AddOperation(string operationType, string valueSourceType, string valueSelection, string text)
        {
            var item = new ListViewItem();

            item.SubItems.Add(string.Empty);
            item.SubItems.Add(string.Empty);

            OperationsListView.Items.Add(PopulateItem(item, operationType, valueSourceType, valueSelection, text));
        }

        public void PopulateOperations(IEnumerable<string> enumerable)
        {
            Operation.DisplayMember = "Text";

            enumerable.ForEach(item => Operation.Items.Add(item));
        }

        public void PopulateValues(IEnumerable<string> enumerable)
        {
            foreach (var item in enumerable)
            {
                ValueSelection.Items.Add(item);
            }
        }

        public void PopulateDecimalTables(IEnumerable<DecimalTableModel> list)
        {
            foreach (var model in list)
            {
                ValueSelection.Items.Add(new SelectionItem(model.Id, model.Name));
            }
        }

        public void PopulateMethods(IEnumerable<MethodDTO> enumerable)
        {
            foreach (var dto in enumerable)
            {
                ValueSelection.Items.Add(new SelectionItem(dto.MethodId, dto.MethodName));
            }
        }

        private object FindSelection(Guid id)
        {
            foreach (SelectionItem item in ValueSelection.Items)
            {
                if (item.Id.Equals(id))
                {
                    return item;
                }
            }

            return null;
        }

        private static ListViewItem PopulateItem(ListViewItem item, string operationType,
                                                 string valueSourceType, string valueSelection, string text)
        {
            item.Text = operationType;

            item.SubItems[1].Text = valueSourceType;
            item.SubItems[2].Text = text;

            item.Tag = new ItemTag(operationType, valueSourceType, valueSelection, text);

            return item;
        }

        private ListViewItem SelectedItem()
        {
            return !HasSelectedItem
                       ? null
                       : OperationsListView.SelectedItems[0];
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (Presenter.CanAddOperation())
            {
                AddOperation(OperationTypeModel.Name, ValueSourceTypeModel.Name, ValueSelectionValue, ValueSelectionText);
            }
        }

        private void OperationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetError(Operation, string.Empty);
        }

        private void ValueSelection_TextChanged(object sender, EventArgs e)
        {
            SetError(ValueSelection, string.Empty);
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            RemoveSelectedItem();
        }

        private void MoveUpButton_Click(object sender, EventArgs e)
        {
            var item = SelectedItem();

            if (!HasSelectedItem || !HasMoreThanOneItem ||
                IsFirstItem(item))
            {
                return;
            }

            MoveItemUp(item);
        }

        private void MoveItemUp(ListViewItem item)
        {
            var index = item.Index - 1;

            OperationsListView.Items.Remove(item);
            OperationsListView.Items.Insert(index, item);
        }

        private void MoveItemDown(ListViewItem item)
        {
            var index = item.Index + 1;

            OperationsListView.Items.Remove(item);
            OperationsListView.Items.Insert(index, item);
        }

        private static bool IsFirstItem(ListViewItem item)
        {
            return item.Index == 0;
        }

        private void MoveDownButton_Click(object sender, EventArgs e)
        {
            var item = SelectedItem();

            if (!HasSelectedItem || !HasMoreThanOneItem ||
                IsLastItem(item))
            {
                return;
            }

            MoveItemDown(item);
        }

        private bool IsLastItem(ListViewItem item)
        {
            return OperationsListView.Items.Count - 1 == item.Index;
        }

        private void OperationsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = SelectedItem();

            var b = item != null;

            if (b)
            {
                OperationValue = item.Text;
                ValueSourceValue = item.SubItems[1].Text;
                ValueSelectionValue = ((ItemTag)item.Tag).ValueSelection;
            }

            MoveDownButton.Enabled = b;
            MoveUpButton.Enabled = b;
            RemoveButton.Enabled = b;
            ApplyButton.Enabled = b;
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            if (!HasSelectedItem)
            {
                return;
            }

            PopulateItem(SelectedItem(), OperationTypeModel.Name, ValueSourceTypeModel.Name, ValueSelectionValue, ValueSelectionText);
        }

        private void ValueSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetError(ValueSource, string.Empty);

            Presenter.ValueSourceChanged();
        }

        private class ItemTag
        {
            public ItemTag(string operationType, string valueSourceType, string valueSelection,
                           string text)
            {
                OperationType = operationType;
                ValueSourceType = valueSourceType;
                ValueSelection = valueSelection;
                Text = text;
            }

            public string OperationType { get; private set; }
            public string ValueSourceType { get; private set; }
            public string ValueSelection { get; private set; }
            public string Text { get; private set; }
        }

        private class SelectionItem
        {
            public SelectionItem(Guid id, string text)
            {
                Id = id;
                Text = text;
            }

            public Guid Id { get; private set; }
            public string Text { get; private set; }
        }
    }

    public class GenericFormulaView : View<IFormulaPresenter>
    {
    }
}
