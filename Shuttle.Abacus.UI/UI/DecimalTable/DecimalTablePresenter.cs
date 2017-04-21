using System;
using System.Collections.Generic;
using System.Data;
using Abacus.Data;
using Abacus.DTO;
using Abacus.Infrastructure;
using Abacus.Localisation;
using Abacus.Validation;

namespace Abacus.UI
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

            var dto = View.RowArgumentDto;

            if (dto.CanOnlyCompareEquality)
            {
                if (dto.HasAnswerCatalog)
                {
                    View.EnableRowAnswerSelection(dto.Answers);

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

                var dto = View.ColumnArgumentDTO;

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

        public bool IsValidAnswer(ArgumentDTO argumentDto, object value)
        {
            if (string.IsNullOrEmpty(argumentDto.AnswerType))
            {
                return true;
            }

            if (argumentDto.HasAnswerCatalog && !HasValidArgumentAnswer(argumentDto, Convert.ToString(value)))
            {
                return false;
            }

            if (argumentDto.IsText)
            {
                return !string.IsNullOrEmpty(Convert.ToString(value));
            }

            return
                valueTypeValidatorProvider.Get(argumentDto.AnswerType).Validate(Convert.ToString(value))
                    .OK;
        }

        private static bool HasValidArgumentAnswer(ArgumentDTO dto, string value)
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

            throw new MissingEntryException(string.Format("Could not find contraint type with name '{0}'.", name));
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

            View.PopulateFactors(Model.Factors);

            if (Model.DecimalTableRow == null)
            {
                return;
            }

            View.DecimalTableNameValue = DecimalTableColumns.Name.MapFrom(Model.DecimalTableRow);

            var rowArgumentDTO = GetArgumentDTO(DecimalTableColumns.RowArgumentId.MapFrom(Model.DecimalTableRow));

            if (rowArgumentDTO == null)
            {
                return;
            }

            View.RowArgumentValue = rowArgumentDTO.Name;

            var columnArgumentDTO = GetArgumentDTO(DecimalTableColumns.ColumnArgumentId.MapFrom(Model.DecimalTableRow));

            if (columnArgumentDTO != null)
            {
                View.ColumnArgumentValue = columnArgumentDTO.Name;
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

        private ArgumentDTO GetArgumentDTO(Guid id)
        {
            foreach (var dto in Model.Factors)
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
