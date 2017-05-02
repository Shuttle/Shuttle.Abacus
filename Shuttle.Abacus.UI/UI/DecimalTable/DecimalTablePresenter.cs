using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.DTO;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Invariants.Interfaces;
using Shuttle.Abacus.Invariants.Values;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.UI.DecimalTable
{
    public class DecimalTablePresenter : Presenter<IDecimalTableView, DecimalTableModel>, IDecimalTablePresenter
    {
        private bool previousRowArgumentWasText;
        private bool previousColumnArgumentWasText;

        private readonly IDecimalTableRules decimalTableRules;
        private readonly IValueTypeValidatorProvider valueTypeValidatorProvider;

        public DecimalTablePresenter(IDecimalTableView view, IDecimalTableRules decimalTableRules, IValueTypeValidatorProvider valueTypeValidatorProvider)
            : base(view)
        {
            this.decimalTableRules = decimalTableRules;
            this.valueTypeValidatorProvider = valueTypeValidatorProvider;

            Text = "Decimal Table";
            Image = Resources.Image_DecimalTable;
        }

        public void DecimalTableNameExited()
        {
            WorkItem.Text = string.Format("Decimal Table{0}",
                                          View.DecimalTableNameValue.Length > 0
                                              ? " : " + View.DecimalTableNameValue
                                              : string.Empty);
        }

        public void RowArgumentChanged()
        {
            if (!View.GridInitialized)
            {
                View.InitializeGrid();
            }

            View.EnableColumnArgument();

            var argumentModel = View.ArgumentModel;

            if (argumentModel.CanOnlyCompareEquality)
            {
                if (argumentModel.HasAnswerCatalog)
                {
                    View.EnableRowAnswerSelection(argumentModel.Answers);

                    previousRowArgumentWasText = false;
                }
                else
                {
                    if (!previousRowArgumentWasText)
                    {
                        View.EnableRowAnswerEntry();
                    }

                    previousRowArgumentWasText = true;
                }

                View.ShowRowAnswerCatalogConstraints();
            }
            else
            {
                if (!previousRowArgumentWasText)
                {
                    View.EnableRowAnswerEntry();

                    previousRowArgumentWasText = true;
                }

                View.ShowRowAllConstraints();
            }
        }

        public void ColumnArgumentChanged()
        {
            if (View.HasColumnArgument)
            {
                View.ApplyColumnArgument();

                var dto = View.ColumnRow;

                if (dto.CanOnlyCompareEquality)
                {
                    if (dto.HasAnswerCatalog)
                    {
                        View.EnableColumnAnswerSelection(dto.Answers);

                        previousColumnArgumentWasText = false;
                    }
                    else
                    {
                        if (!previousColumnArgumentWasText)
                        {
                            View.EnableColumnAnswerEntry();
                        }

                        previousColumnArgumentWasText = true;
                    }

                    View.ShowColumnAnswerCatalogConstraints();
                }
                else
                {
                    if (!previousColumnArgumentWasText)
                    {
                        View.EnableColumnAnswerEntry();

                        previousColumnArgumentWasText = true;
                    }

                    View.ShowColumnAllConstraints();
                }
            }
            else
            {
                View.RowFactorsOnly();
            }
        }

        public bool IsDecimal(string value)
        {
            decimal dec;

            return decimal.TryParse(value, out dec);
        }

        public bool IsValidAnswer(DataRow row, object value)
        {
            if (string.IsNullOrEmpty(row.AnswerType))
            {
                return true;
            }

            if (row.HasAnswerCatalog && !HasValidArgumentAnswer(row, Convert.ToString(value)))
            {
                return false;
            }

            if (row.IsText)
            {
                return !string.IsNullOrEmpty(Convert.ToString(value));
            }

            return
                valueTypeValidatorProvider.Get(row.AnswerType).Validate(Convert.ToString(value))
                    .OK;
        }

        private static bool HasValidArgumentAnswer(DataRow dto, string value)
        {
            foreach (var answer in dto.Answers)
            {
                if (answer.Answer.Equals(value, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public void ShowInvalidDecimalTableMessage()
        {
            MessageBus.Publish(
                Result.Create().AddFailureMessage("The decimal table has invalid values.  Please investigate."));
        }

        public ConstraintTypeDTO GetConstraintType(string name)
        {
            foreach (var dto in ConstraintTypes)
            {
                if (dto.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return dto;
                }
            }

            throw new MissingEntityException(string.Format("Could not find contraint type with name '{0}'.", name));
        }

        public IEnumerable<ConstraintTypeDTO> ConstraintTypes
        {
            get { return Model.ConstraintTypes; }
        }

        public override void OnInitialize()
        {
            base.OnInitialize();

            Guard.AgainstNull(Model, "Model");

            View.DecimalTableNameRules = decimalTableRules.DecimalTableNameRules();
            View.RowArgumentRules = decimalTableRules.RowArgumentRules();

            View.PopulateArguments(Model.ArgumentRows);

            if (Model.DecimalTableRow == null)
            {
                return;
            }

            View.DecimalTableNameValue = DecimalTableColumns.Name.MapFrom(Model.DecimalTableRow);

            var rowDataRow = GetDataRow(DecimalTableColumns.RowArgumentId.MapFrom(Model.DecimalTableRow));

            if (rowDataRow == null)
            {
                return;
            }

            View.RowArgumentValue = rowDataRow.Name;

            var columnDataRow = GetDataRow(DecimalTableColumns.ColumnArgumentId.MapFrom(Model.DecimalTableRow));

            if (columnDataRow != null)
            {
                View.ColumnArgumentValue = columnDataRow.Name;
            }

            foreach (DataRow row in Model.ConstrainedDecimalValues.Rows)
            {
                View.AddDecimalValue(
                    DecimalValueColumns.ColumnIndex.MapFrom(row),
                    DecimalValueColumns.RowIndex.MapFrom(row),
                    DecimalValueColumns.DecimalValue.MapFrom(row),
                    ConstraintColumns.Name.MapFrom(row),
                    ConstraintColumns.ArgumentName.MapFrom(row),
                    ConstraintColumns.Answer.MapFrom(row)
                    );
            }
        }

        private DataRow GetDataRow(Guid id)
        {
            foreach (var dto in Model.ArgumentRows)
            {
                if (dto.Id.Equals(id))
                {
                    return dto;
                }
            }

            return null;
        }
    }
}
