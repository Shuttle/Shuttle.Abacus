using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.Messages.v1.TransferObjects;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;

namespace Shuttle.Abacus.UI.UI.Matrix
{
    public partial class MatrixView : GenericDecimalTableView, IMatrixView
    {
        private readonly DataGridViewCellStyle errorCellStyle;

        public MatrixView()
        {
            InitializeComponent();

            GridInitialized = false;

            ValueGridView.DataError += ValueGridView_DataError;

            ValueGridView.CellValidating += ValueGridView_CellValidating;

            errorCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.Red,
                ForeColor = Color.White
            };
        }

        static void ValueGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        protected bool OnFixedRow => (ValueGridView.CurrentCell.RowIndex < 2);

        protected bool OnLastRow => (ValueGridView.CurrentCell.RowIndex == ValueGridView.RowCount - 1);

        protected bool OnLastColumn => ValueGridView.ColumnCount == SelectedColumn().DisplayIndex + 1;

        private bool OnFixedColumn => (SelectedColumn().DisplayIndex < 2);

        protected bool CanAddColumn => HasColumnArgument;

        protected bool OnFirstRow => ValueGridView.CurrentCell.RowIndex == 2;

        protected bool OnFirstColumn => SelectedColumn().DisplayIndex == 2;

        public string DecimalTableNameValue
        {
            get { return DecimalTableName.Text; }
            set { DecimalTableName.Text = value; }
        }

        public IRuleCollection<object> DecimalTableNameRules
        {
            set { ViewValidator.Control(DecimalTableName).ShouldSatisfy(value); }
        }

        public IRuleCollection<object> RowArgumentRules
        {
            set { ViewValidator.Control(RowArgument).ShouldSatisfy(value); }
        }

        public ArgumentModel RowArgumentModel => (ArgumentModel)RowArgument.SelectedItem;

        public ArgumentModel ColumnArgumentModel => (ArgumentModel)ColumnArgument.SelectedItem;

        public bool GridInitialized { get; private set; }

        public bool HasColumnArgument => ColumnArgument.SelectedIndex > 0;

        public void PopulateArguments(IEnumerable<ArgumentModel> rows)
        {
            RowArgument.DisplayMember = "Name";
            ColumnArgument.DisplayMember = "Name";

            //TODO
            //ColumnArgument.Items.Add(new DataRow
            //                       {
            //                           Name = "(none)",
            //                           AnswerType = string.Empty
            //                       });

            rows.ForEach(row =>
                {
                    RowArgument.Items.Add(row);
                    ColumnArgument.Items.Add(row);
                });

            ColumnArgument.SelectedIndex = 0;
        }

        public void EnableColumnArgument()
        {
            ColumnArgument.Enabled = true;
        }

        public void PopulateConstraintTypes(DataGridViewComboBoxCell combo, bool restrictToAnswerCatalog)
        {
            combo.Items.Clear();
            combo.DisplayMember = "Text";

            Presenter.ConstraintTypes.ForEach(item =>
            {
                if (!restrictToAnswerCatalog || item.EnabledForRestrictedAnswers)
                {
                    combo.Items.Add(item);
                }
            });
        }

        public void ShowRowAnswerCatalogConstraints()
        {
            RowConstraints().ForEach(combo => PopulateConstraintTypes(combo, true));
        }

        public void ShowRowAllConstraints()
        {
            RowConstraints().ForEach(combo => PopulateConstraintTypes(combo, false));
        }

        public void ShowColumnAnswerCatalogConstraints()
        {
            ColumnConstraints().ForEach(combo => PopulateConstraintTypes(combo, true));
        }

        public void ShowColumnAllConstraints()
        {
            ColumnConstraints().ForEach(combo => PopulateConstraintTypes(combo, false));
        }

        public void EnableRowAnswerSelection(IEnumerable<string> answers)
        {
            for (var row = 2; row <= ValueGridView.RowCount - 1; row++)
            {
                MakeAnswerSelectionCell(1, row, answers);
            }
        }

        public void EnableColumnAnswerSelection(IEnumerable<string> answers)
        {
            for (var column = 2; column <= ValueGridView.ColumnCount - 1; column++)
            {
                MakeAnswerSelectionCell(column, 1, answers);
            }
        }

        public void EnableRowAnswerEntry()
        {
            for (var row = 2; row <= ValueGridView.RowCount - 1; row++)
            {
                MakeAnswerEntryCell(1, row);
            }
        }

        public void EnableColumnAnswerEntry()
        {
            for (var column = 2; column <= ValueGridView.ColumnCount - 1; column++)
            {
                MakeAnswerEntryCell(column, 1);
            }
        }

        public void InitializeGrid()
        {
            ValueGridView.Enabled = true;

            ValueGridView.Columns.Add("RowConstraint", string.Empty);
            ValueGridView.Columns.Add("RowArgument", string.Empty);

            ValueGridView.Rows.Add(2);

            ValueGridView[0, 0].Value = "Where";
            ValueGridView[0, 0].ReadOnly = true;

            ValueGridView[0, 1].Value = RowArgumentValue;
            ValueGridView[0, 1].ReadOnly = true;

            ValueGridView[1, 1].Value = "Value";
            ValueGridView[1, 1].ReadOnly = true;
            ValueGridView[1, 0].ReadOnly = true;

            ValueGridView.Rows[0].Frozen = true;
            ValueGridView.Rows[1].Frozen = true;

            ValueGridView.Columns[0].Frozen = true;
            ValueGridView.Columns[1].Frozen = true;

            AddRow();

            GridInitialized = true;

            RowFactorsOnly();
        }

        public void RowFactorsOnly()
        {
            if (!GridInitialized)
            {
                return;
            }

            while (ValueGridView.Columns.Count > 2)
            {
                ValueGridView.Columns.RemoveAt(2);
            }

            ValueGridView.Columns.Add("Value", string.Empty);

            ValueGridView[2, 1].Value = "Value";
            ValueGridView[2, 1].ReadOnly = true;
            ValueGridView[2, 0].ReadOnly = true;
        }

        public void ApplyColumnArgument()
        {
            ValueGridView[1, 0].Value = ColumnArgumentValue;
            ValueGridView[1, 0].ReadOnly = true;

            for (var column = 2; column <= ValueGridView.ColumnCount - 1; column++)
            {
                MakeConstraintCell(column, 0);

                //TODO
                //if (ColumnRow.HasAnswerCatalog)
                //{
                //    MakeAnswerSelectionCell(column, 1, ColumnRow.ArgumentValues);
                //}
                //else
                //{
                //    MakeAnswerEntryCell(column, 1);
                //}
            }
        }

        public bool HasInvalidDecimalTable()
        {
            ValueGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);

            return
                HasSameRowAndColumnArgument() ||
                HasInvalidRowAnswers() ||
                HasInvalidRowConstraints() ||
                HasInvalidColumnAnswers() ||
                HasInvalidColumnConstraints() ||
                HasInvalidValues();
        }

        private bool HasSameRowAndColumnArgument()
        {
            if (RowArgumentModel.Id.Equals(ColumnArgumentModel.Id))
            {
                const string message = "Cannot have the same row and column argument.";

                SetError(RowArgument, message);
                SetError(ColumnArgument, message);

                return true;
            }

            return false;
        }

        public List<MatrixElement> DecimalValues()
        {
            var result = new List<MatrixElement>();

            for (var column = 2; column <= ValueGridView.ColumnCount - 1; column++)
            {
                for (var row = 2; row <= ValueGridView.RowCount - 1; row++)
                {
                    var cell = ValueGridView[column, row];

                    var decimalValue = new MatrixElement
                    {
                        Row = row,
                        Column = column,
                        Value = decimal.Parse(Convert.ToString(cell.Value))
                    };

                    decimalValue.Constraints.Add(new Abacus.Messages.v1.TransferObjects.FormulaConstraint
                    {
                        ArgumentName = RowArgumentModel.Name,
                        ComparisonType = RowComparisonType(row),
                        Value = RowConstraintValue(row)
                    });

                    if (HasColumnArgument)
                    {
                        decimalValue.Constraints.Add(new Abacus.Messages.v1.TransferObjects.FormulaConstraint
                        {
                            ArgumentName = ColumnArgumentModel.Name,
                            ComparisonType = ColumnComparisonType(column),
                            Value = ColumnConstraintValue(column)
                        });
                    }

                    result.Add(decimalValue);
                }
            }

            return result;
        }

        public void AddElement(int column, int row, decimal value)
        {
            EnsureRowsTo(row);
            EnsureColumnsTo(column);

            //if (IsRowArgument(argument))
            //{
            //    SetRowConstraint(row, constraint, answer);
            //}

            //if (IsColumnArgument(argument))
            //{
            //    SetColumnConstraint(column, constraint, answer);
            //}

            if (ValidCoordinates(column, row))
            {
                ValueGridView[column, row].Value = value;
            }
        }

        private bool ValidCoordinates(int column, int row)
        {
            return ValueGridView.ColumnCount > column && ValueGridView.RowCount > row;
        }

        private void SetRowConstraint(int row, string constraint, string answer)
        {
            ValueGridView[0, row].Value = constraint;
            ValueGridView[1, row].Value = answer;
        }

        private bool IsRowArgument(string argument)
        {
            return RowArgumentValue.Equals(argument, StringComparison.InvariantCultureIgnoreCase);
        }

        private void SetColumnConstraint(int column, string constraint, string answer)
        {
            ValueGridView[column, 0].Value = constraint;
            ValueGridView[column, 1].Value = answer;
        }

        private bool IsColumnArgument(string argument)
        {
            return ColumnArgumentValue.Equals(argument, StringComparison.InvariantCultureIgnoreCase);
        }

        private void EnsureRowsTo(int row)
        {
            while (ValueGridView.RowCount <= row)
            {
                AddRow();
            }
        }

        private void EnsureColumnsTo(int column)
        {
            while (ValueGridView.ColumnCount <= column)
            {
                AddColumn();
            }
        }

        private string ColumnConstraintValue(int column)
        {
            return Convert.ToString(ValueGridView[column, 1].Value);
        }

        private string RowConstraintValue(int row)
        {
            return Convert.ToString(ValueGridView[1, row].Value);
        }

        public string RowArgumentValue
        {
            get { return RowArgument.Text; }
            set { RowArgument.Text = value; }
        }

        public string ColumnArgumentValue
        {
            get { return ColumnArgument.Text; }
            set { ColumnArgument.Text = value; }
        }

        private string RowComparisonType(int row)
        {
            return (string)ValueGridView[0, row].Value;
        }

        private string ColumnComparisonType(int column)
        {
            return (string)ValueGridView[column, 0].Value;
        }

        private bool HasInvalidColumnConstraints()
        {
            if (!HasColumnArgument)
            {
                return false;
            }

            for (var column = 2; column <= ValueGridView.ColumnCount - 1; column++)
            {
                var cell = ValueGridView[column, 0];

                if (!string.IsNullOrEmpty(Convert.ToString(cell.Value)))
                {
                    cell.Style = null;

                    continue;
                }

                cell.Style = errorCellStyle;

                return true;
            }

            return false;
        }

        private bool HasInvalidRowConstraints()
        {
            for (var row = 2; row <= ValueGridView.RowCount - 1; row++)
            {
                var cell = ValueGridView[0, row];

                if (!string.IsNullOrEmpty(Convert.ToString(cell.Value)))
                {
                    cell.Style = null;

                    continue;
                }

                cell.Style = errorCellStyle;

                return true;
            }

            return false;
        }

        private bool HasInvalidValues()
        {
            for (var column = 2; column <= ValueGridView.ColumnCount - 1; column++)
            {
                for (var row = 2; row <= ValueGridView.RowCount - 1; row++)
                {
                    var cell = ValueGridView[column, row];

                    if (Presenter.IsDecimal(Convert.ToString(cell.Value)))
                    {
                        cell.Style = null;

                        continue;
                    }

                    cell.Style = errorCellStyle;

                    return true;
                }
            }

            return false;
        }

        private bool HasInvalidColumnAnswers()
        {
            if (!HasColumnArgument)
            {
                return ValueGridView.ColumnCount != 3;
            }

            for (var column = 2; column <= ValueGridView.ColumnCount - 1; column++)
            {
                var cell = ValueGridView[column, 1];

                if (Presenter.IsValidAnswer(ColumnArgumentModel, cell.Value))
                {
                    cell.Style = null;

                    continue;
                }

                cell.Style = errorCellStyle;

                return true;
            }

            return false;
        }

        private bool HasInvalidRowAnswers()
        {
            for (var row = 2; row <= ValueGridView.RowCount - 1; row++)
            {
                var cell = ValueGridView[1, row];

                if (Presenter.IsValidAnswer(RowArgumentModel, cell.Value))
                {
                    cell.Style = null;

                    continue;
                }

                cell.Style = errorCellStyle;

                return true;
            }

            return false;
        }

        private void ValueGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var value = e.FormattedValue;

            if (value == null || string.IsNullOrEmpty(Convert.ToString(value)))
            {
                return;
            }

            var ok = true;

            if (IsValueCell(e.ColumnIndex, e.RowIndex))
            {
                ok = Presenter.IsDecimal(Convert.ToString(value));
            }

            if (IsRowAnswerCell(e.ColumnIndex, e.RowIndex))
            {
                ok = Presenter.IsValidAnswer(RowArgumentModel, value);
            }

            if (IsColumnAnswerCell(e.ColumnIndex, e.RowIndex))
            {
                ok = Presenter.IsValidAnswer(ColumnArgumentModel, value);
            }

            var cell = ValueGridView[e.ColumnIndex, e.RowIndex];

            if (!ok)
            {
                Presenter.ShowInvalidDecimalTableMessage();

                e.Cancel = true;

                cell.Style = errorCellStyle;
            }
            else
            {
                cell.Style = null;
            }
        }

        private IEnumerable<DataGridViewComboBoxCell> RowConstraints()
        {
            var result = new List<DataGridViewComboBoxCell>();

            for (var row = 2; row <= ValueGridView.RowCount - 1; row++)
            {
                result.Add((DataGridViewComboBoxCell)ValueGridView[0, row]);
            }

            return result;
        }

        private IEnumerable<DataGridViewComboBoxCell> ColumnConstraints()
        {
            var result = new List<DataGridViewComboBoxCell>();

            for (var column = 2; column <= ValueGridView.ColumnCount - 1; column++)
            {
                result.Add((DataGridViewComboBoxCell)ValueGridView[column, 0]);
            }

            return result;
        }

        private void MakeAnswerSelectionCell(int column, int row, IEnumerable<string> answers)
        {
            var cell = new DataGridViewComboBoxCell
            {
                DisplayMember = "Name",
                DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
            };

            answers.ForEach(item => cell.Items.Add(item));

            ValueGridView[column, row] = cell;
            ValueGridView[column, row].ReadOnly = false;
        }

        private void MakeAnswerEntryCell(int column, int row)
        {
            ValueGridView[column, row] = new DataGridViewTextBoxCell
            {
                ReadOnly = false
            };

            ValueGridView[column, row].ReadOnly = false;
        }

        private bool IsRowAnswerCell(int columnIndex, int rowIndex)
        {
            return columnIndex == 1 && rowIndex > 1;
            //TODO
            //return !RowArgumentModel.HasAnswerCatalog && columnIndex == 1 && rowIndex > 1;
        }

        private bool IsColumnAnswerCell(int columnIndex, int rowIndex)
        {
            return columnIndex > 1 && rowIndex == 1;
            //TODO
            //return !ColumnArgumentModel.HasAnswerCatalog && columnIndex > 1 && rowIndex == 1;
        }

        private static bool IsValueCell(int columnIndex, int rowIndex)
        {
            return columnIndex > 1 && rowIndex > 1;
        }

        private void MakeConstraintCell(int column, int row)
        {
            var cell = new DataGridViewComboBoxCell
            {
                DisplayMember = "Text",
                DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
            };

            ValueGridView[column, row] = cell;
            ValueGridView[column, row].ReadOnly = false;

            PopulateConstraintTypes(cell, Presenter.RowAnswers().Any());
        }

        private void AddRow()
        {
            var row = ValueGridView.RowCount;

            ValueGridView.Rows.Add(1);

            MakeConstraintCell(0, row);

            if (Presenter.RowAnswers().Any())
            {
                MakeAnswerSelectionCell(1, row, Presenter.RowAnswers());
            }
            else
            {
                MakeAnswerEntryCell(1, row);
            }
        }

        private void DecimalTableName_Leave(object sender, EventArgs e)
        {
            Presenter.DecimalTableNameExited();
        }

        private void RowArgument_SelectedIndexChanged(object sender, EventArgs e)
        {
            Presenter.RowArgumentChanged();

            if (GridInitialized)
            {
                ValueGridView[0, 1].Value = RowArgumentValue;
            }
        }

        private void AddRowMenuItem_Click(object sender, EventArgs e)
        {
            AddRow();
        }

        private void RemoveRowMenuItem_Click(object sender, EventArgs e)
        {
            RemoveSelectedRow();
        }

        private void RemoveSelectedRow()
        {
            if (ValueGridView.RowCount < 4)
            {
                return;
            }

            var currentRow = ValueGridView.CurrentRow;

            if (currentRow == null)
            {
                return;
            }

            var rowIndex = currentRow.Index;
            var columnIndex = ValueGridView.CurrentCell.ColumnIndex;

            ValueGridView.Rows.RemoveAt(rowIndex);

            if (rowIndex < ValueGridView.RowCount)
            {
                ValueGridView.CurrentCell = ValueGridView[columnIndex, rowIndex];
            }
        }

        private void MoveRowUpMenuItem_Click(object sender, EventArgs e)
        {
            if (OnFixedRow || OnFirstRow)
            {
                return;
            }

            MoveRow(ValueGridView.CurrentRow, Direction.Up);
        }

        private void MoveRow(DataGridViewRow row, Direction direction)
        {
            if (row == null)
            {
                return;
            }

            var rowIndex = ValueGridView.CurrentCell.RowIndex;
            var columnIndex = ValueGridView.CurrentCell.ColumnIndex;

            ValueGridView.Rows.RemoveAt(rowIndex);

            rowIndex += direction == Direction.Up
                            ? -1
                            : 1;

            ValueGridView.Rows.Insert(rowIndex, row);

            ValueGridView.CurrentCell = ValueGridView[columnIndex, rowIndex];

            ValueGridView.FirstDisplayedCell = ValueGridView.CurrentCell;
        }

        private void MoveRowDownMenuItem_Click(object sender, EventArgs e)
        {
            if (OnFixedRow || OnLastRow)
            {
                return;
            }

            MoveRow(ValueGridView.CurrentRow, Direction.Down);
        }

        private void ColumnArgument_SelectedIndexChanged(object sender, EventArgs e)
        {
            Presenter.ColumnArgumentChanged();

            if (GridInitialized)
            {
                ValueGridView[1, 0].Value = ColumnArgumentValue;
            }
        }

        private void AddColumnMenuItem_Click(object sender, EventArgs e)
        {
            AddUserColumn();
        }

        private void AddUserColumn()
        {
            if (!CanAddColumn)
            {
                return;
            }

            AddColumn();
        }

        private void AddColumn()
        {
            var column = ValueGridView.ColumnCount;

            ValueGridView.Columns.Add(string.Empty, string.Empty);

            MakeConstraintCell(column, 0);

            if (Presenter.ColumnAnswers().Any())
            {
                MakeAnswerSelectionCell(column, 1, Presenter.ColumnAnswers());
            }
            else
            {
                MakeAnswerEntryCell(column, 1);
            }
        }

        private void RemoveColumnMenuItem_Click(object sender, EventArgs e)
        {
            RemoveSelectedColumn();
        }

        private void RemoveSelectedColumn()
        {
            if (ValueGridView.ColumnCount < 4)
            {
                return;
            }

            var currentRow = ValueGridView.CurrentRow;

            if (currentRow == null)
            {
                return;
            }

            var rowIndex = currentRow.Index;
            var columnIndex = ValueGridView.CurrentCell.ColumnIndex;

            ValueGridView.Columns.RemoveAt(columnIndex);

            if (columnIndex < ValueGridView.ColumnCount)
            {
                ValueGridView.CurrentCell = ValueGridView[columnIndex, rowIndex];
            }
        }

        private void MoveColumnLeftMenuItem_Click(object sender, EventArgs e)
        {
            if (OnFixedColumn || OnFirstColumn)
            {
                return;
            }

            MoveColumn(SelectedColumn(), Direction.Left);
        }

        private DataGridViewColumn SelectedColumn()
        {
            return ValueGridView.Columns[ValueGridView.CurrentCell.ColumnIndex];
        }

        private void MoveColumn(DataGridViewColumn column, Direction direction)
        {
            column.DisplayIndex += direction == Direction.Left
                                       ? -1
                                       : 1;

            ValueGridView.FirstDisplayedCell = ValueGridView.CurrentCell;
        }

        private void MoveColumnRightMenuItem_Click(object sender, EventArgs e)
        {
            if (OnFixedColumn || OnLastColumn)
            {
                return;
            }

            MoveColumn(SelectedColumn(), Direction.Right);
        }

        private enum Direction
        {
            Up = 0,
            Down = 1,
            Left = 2,
            Right = 3
        }
    }

    public class GenericDecimalTableView : View<IMatrixPresenter>
    {
    }
}
