using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Models;

namespace Shuttle.Abacus.Shell.UI.FormulaOperation
{
    public partial class FormulaOperationView : GenericFormulaOperationView, IFormulaOperationView
    {
        public FormulaOperationView()
        {
            InitializeComponent();
        }

        protected bool HasMoreThanOneItem => OperationsListView.Items.Count > 1;

        public string OperationValue
        {
            get { return Operation.Text; }
            set { Operation.Text = value; }
        }

        public bool HasOperation => Operation.SelectedIndex > -1;

        public OperationTypeModel OperationTypeModel => Operation.SelectedItem as OperationTypeModel;

        public bool HasSelectedItem => OperationsListView.SelectedItems.Count > 0;

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

        public IEnumerable<FormulaOperationModel> FormulaOperations
        {
            get
            {
                foreach (ListViewItem item in OperationsListView.Items)
                {
                    yield return (FormulaOperationModel) item.Tag;
                }
            }
            set { value.ForEach(model => AddOperation(model.Operation, model.ValueSource, model.ValueSelection)); }
        }

        public string ValueSourceValue
        {
            get { return ValueSource.Text; }
            set { ValueSource.Text = value; }
        }

        public string ValueSelectionValue
        {
            get { return ValueSelection.Text; }
            set
            {
                ValueSelection.SelectedItem = value;
                ValueSelection.Text = value;
            }
        }

        public string ValueSelectionText => ValueSelection.Text;

        public bool HasValueSource => ValueSource.SelectedIndex > -1;

        public bool HasValueSelection => ValueSelection.Text.Length > 0;

        public void PopulateValueSources(IEnumerable<ValueSourceTypeModel> models)
        {
            ValueSource.DisplayMember = "Text";

            models.ForEach(source => ValueSource.Items.Add(source));
        }

        public void PopulateArguments(IEnumerable<ArgumentModel> arguments)
        {
            arguments.ForEach(model => { ValueSelection.Items.Add(model.Name); });
        }

        public void ShowValueSourceError()
        {
            SetError(ValueSource, "Please select the value source.");
        }

        public void ShowValueSelectionError(string message)
        {
            SetError(ValueSelection, message);
        }

        public void AddOperation(string operation, string valueSource, string valueSelection)
        {
            var item = new ListViewItem();

            item.SubItems.Add(string.Empty);
            item.SubItems.Add(string.Empty);

            OperationsListView.Items.Add(PopulateItem(item, operation, valueSource, valueSelection));
        }

        public void PopulateOperations(IEnumerable<string> enumerable)
        {
            Operation.DisplayMember = "Text";

            enumerable.ForEach(item => Operation.Items.Add(item));
        }

        public void PopulateValues(IEnumerable<string> values)
        {
            foreach (var value in values)
            {
                ValueSelection.Items.Add(value);
            }
        }

        public void PopulateMatrixes(IEnumerable<MatrixModel> matrixes)
        {
            foreach (var model in matrixes)
            {
                ValueSelection.Items.Add(model.Name);
            }
        }

        public void ClearValueSelection()
        {
            ValueSelection.Items.Clear();
        }

        public string NameValue { get; set; }

        public void PopulateFormulas(IEnumerable<FormulaModel> formulas)
        {
            foreach (var model in formulas)
            {
                ValueSelection.Items.Add(model.Name);
            }
        }

        private static ListViewItem PopulateItem(ListViewItem item, string operation, string valueSource,
            string valueSelection)
        {
            item.Text = operation;

            item.SubItems[1].Text = valueSource;
            item.SubItems[2].Text = valueSelection;

            item.Tag = new FormulaOperationModel
            {
                Operation = operation,
                ValueSelection = valueSelection,
                ValueSource = valueSource
            };

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
                AddOperation(OperationValue, ValueSourceValue, ValueSelectionValue);
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
                ValueSelectionValue = ((FormulaOperationModel) item.Tag).ValueSelection;
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

            PopulateItem(SelectedItem(), OperationValue, ValueSourceValue, ValueSelectionValue);
        }

        private void ValueSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetError(ValueSource, string.Empty);

            Presenter.ValueSourceChanged();
        }
    }

    public class GenericFormulaOperationView : View<IFormulaOperationPresenter>
    {
    }
}