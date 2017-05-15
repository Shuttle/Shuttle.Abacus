using System.Linq;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Invariants.Values;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Core.Formatters;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.UI.FormulaConstraint
{
    public class FormulaConstraintPresenter : Presenter<IFormulaConstraintView, ManageFormulaConstraintsModel>, IFormulaConstraintPresenter
    {
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IArgumentQuery _argumentQuery;
        private readonly IConstraintTypeQuery _constraintTypeQuery;
        private readonly IValueTypeValidatorProvider _valueTypeValidatorProvider;

        public FormulaConstraintPresenter(IFormulaConstraintView view, IDatabaseContextFactory databaseContextFactory, IArgumentQuery argumentQuery, IConstraintTypeQuery constraintTypeQuery, IValueTypeValidatorProvider valueTypeValidatorProvider)
            : base(view)
        {
            Guard.AgainstNull(view, "view");
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(argumentQuery, "argumentQuery");
            Guard.AgainstNull(constraintTypeQuery, "constraintTypeQuery");
            Guard.AgainstNull(valueTypeValidatorProvider, "valueTypeValidatorProvider");

            _databaseContextFactory = databaseContextFactory;
            _argumentQuery = argumentQuery;
            _constraintTypeQuery = constraintTypeQuery;
            _valueTypeValidatorProvider = valueTypeValidatorProvider;

            Text = "Constraint Details";
            Image = Resources.Image_FormulaConstraint;
        }

        public void ArgumentChanged()
        {
            var model = View.ArgumentModel;

            View.DetachValueFormatter();

            var answerRows = _argumentQuery.GetValues(model.Id).ToList();

            if (answerRows.Any() || model.IsText())
            {
                if (answerRows.Any())
                {
                    View.EnableAnswerSelection();

                    View.PopulateAnswers(answerRows);
                }
                else
                {
                    View.EnableAnswerEntry();
                }

                View.ShowAnswerCatalogConstraints();
            }
            else
            {
                if (model.IsMoney())
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

            if (View.HasAnswers)
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
                    var answerType = View.ArgumentModel.AnswerType;

                    var validator = _valueTypeValidatorProvider.Has(answerType)
                                        ? _valueTypeValidatorProvider.Get(answerType)
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

            if (Model == null)
            {
                throw new NullDependencyException("Model");
            }

            View.PopulateArguments(Model.Arguments);
            View.PopulateContraintTypes(Model.ConstraintTypes);
            View.Constraints = Model.Constraints;
        }
    }
}
