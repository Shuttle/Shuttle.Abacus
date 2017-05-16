using System.Collections.Generic;
using System.Data;
using System.Linq;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Invariants.Values;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.UI.FormulaConstraint
{
    public class FormulaConstraintPresenter : Presenter<IFormulaConstraintView, ManageFormulaConstraintsModel>,
        IFormulaConstraintPresenter
    {
        private readonly IArgumentQuery _argumentQuery;
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IValueTypeValidatorProvider _valueTypeValidatorProvider;

        public FormulaConstraintPresenter(IFormulaConstraintView view, IDatabaseContextFactory databaseContextFactory,
            IArgumentQuery argumentQuery,
            IValueTypeValidatorProvider valueTypeValidatorProvider)
            : base(view)
        {
            Guard.AgainstNull(view, "view");
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(argumentQuery, "argumentQuery");
            Guard.AgainstNull(valueTypeValidatorProvider, "valueTypeValidatorProvider");

            _databaseContextFactory = databaseContextFactory;
            _argumentQuery = argumentQuery;
            _valueTypeValidatorProvider = valueTypeValidatorProvider;

            Text = "Constraint Details";
            Image = Resources.Image_FormulaConstraint;
        }

        public void PopulateArgumentValues()
        {
            var model = View.ArgumentModel;

            using (_databaseContextFactory.Create())
            {
                View.PopulateArgumentValues(_argumentQuery.GetValues(model.Id));
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
            View.FormulaConstraints = Model.Constraints;
        }
    }
}