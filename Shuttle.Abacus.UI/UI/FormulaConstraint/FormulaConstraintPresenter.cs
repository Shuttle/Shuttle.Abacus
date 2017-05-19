using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Invariants.Interfaces;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Models;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.UI.FormulaConstraint
{
    public class FormulaConstraintPresenter : Presenter<IFormulaConstraintView, ManageFormulaConstraintsModel>,
        IFormulaConstraintPresenter
    {
        private readonly IArgumentQuery _argumentQuery;
        private readonly IValueTypeRules _valueTypeRules;
        private readonly IDatabaseContextFactory _databaseContextFactory;

        public FormulaConstraintPresenter(IFormulaConstraintView view, IDatabaseContextFactory databaseContextFactory, IArgumentQuery argumentQuery, IValueTypeRules valueTypeRules)
            : base(view)
        {
            Guard.AgainstNull(view, "view");
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(argumentQuery, "argumentQuery");
            Guard.AgainstNull(valueTypeRules, "valueTypeRules");

            _databaseContextFactory = databaseContextFactory;
            _argumentQuery = argumentQuery;
            _valueTypeRules = valueTypeRules;

            Text = "Constraint Details";
            Image = Resources.Image_FormulaConstraint;
        }

        public void PopulateArgumentValues()
        {
            var model = View.ArgumentModel;

            using (_databaseContextFactory.Create())
            {
                View.PopulateArgumentValues(
                    _argumentQuery.GetValues(model.Id).Map(row => ArgumentColumns.ValueColumns.Value.MapFrom(row)));
            }
        }

        public bool ConstraintOK()
        {
            if (!View.HasArgument)
            {
                View.ShowArgumentError();

                return false;
            }

            if (!View.HasComparison)
            {
                View.ShowComparisonError();

                return false;
            }

            if (!View.HasArgumentValue)
            {
                View.ShowArgumentValueError("Please enter a value.");

                return false;
            }

            if (View.HasArgument)
            {
                var rules = _valueTypeRules.For(View.ArgumentModel.ValueType);

                if (rules != null)
                {
                    var result = rules.BrokenBy(View.ArgumentValue);

                    if (!result.IsEmpty)
                    {
                        View.ShowArgumentValueError(result.Messages[0].Text);

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