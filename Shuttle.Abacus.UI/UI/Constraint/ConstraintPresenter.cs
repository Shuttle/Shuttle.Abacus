using Shuttle.Abacus.UI.Core.Formatters;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;

namespace Shuttle.Abacus.UI.UI.Constraint
{
    public class ConstraintPresenter : Presenter<IConstraintView, ConstraintModel>, IConstraintPresenter
    {
        private readonly IValueTypeValidatorProvider valueTypeValidatorProvider;

        public ConstraintPresenter(IConstraintView view, IValueTypeValidatorProvider valueTypeValidatorProvider)
            : base(view)
        {
            this.valueTypeValidatorProvider = valueTypeValidatorProvider;

            Text = "Constraint Details";
            Image = Resources.Image_Constraint;
        }

        public void ArgumentChanged()
        {
            var dto = View.ArgumentDto;

            View.DetachValueFormatter();

            if (dto.CanOnlyCompareEquality)
            {
                if (dto.HasAnswerCatalog)
                {
                    View.EnableAnswerSelection();

                    View.PopulateAnswerCatalogValues(dto.Answers);
                }
                else
                {
                    View.EnableAnswerEntry();
                }

                View.ShowAnswerCatalogConstraints();
            }
            else
            {
                if (dto.IsMoney)
                {
                    View.AttachValueFormatter(new MoneyFormatter(View.ValueSelectionControl, View.FormattedControl));
                }

                View.EnableAnswerEntry();

                View.ShowAllConstraints();
            }
        }

        public bool ConstraintOK()
        {
            if (!View.HasArgument)
            {
                View.ShowArgumentError();

                return false;
            }

            if (!View.HasConstraint)
            {
                View.ShowConstraintError();

                return false;
            }

            if (View.ArgumentDto.HasAnswerCatalog)
            {
                if (!View.HasAnswer)
                {
                    View.ShowAnswerError("Please make a selection.");

                    return false;
                }
            }
            else
            {
                if (!View.HasAnswer)
                {
                    View.ShowAnswerError("Please enter a value.");

                    return false;
                }

                if (View.HasArgument)
                {
                    var validator = valueTypeValidatorProvider.Has(View.ArgumentDto.AnswerType)
                                        ? valueTypeValidatorProvider.Get(View.ArgumentDto.AnswerType)
                                        : null;

                    if (validator != null)
                    {
                        var result = validator.Validate(View.AnswerValue);

                        if (!result.OK)
                        {
                            View.ShowAnswerError(result.ToString());

                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public override void OnInitialize()
        {
            base.OnInitialize();

            Guard.AgainstNullDependency(Model, "Model");

            View.PopulateFactors(Model.Arguments);
            View.SetContraintTypes(Model.ConstraintTypes);

            if (Model.Constraints == null)
            {
                return;
            }

            foreach (var constraint in Model.Constraints)
            {
                View.AddConstraint(constraint.ArgumentDto, constraint.ConstraintTypeDTO, constraint.Value);
            }
        }
    }
}
