using Shuttle.Abacus.Invariants;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Models;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.UI.Formula
{
    public class FormulaPresenter : Presenter<IFormulaView, FormulaModel>, IFormulaPresenter
    {
        private readonly IFormulaRules _formulaRules;

        public FormulaPresenter(IFormulaView view, IFormulaRules formulaRules)
            : base(view)
        {
            Guard.AgainstNull(formulaRules, "formulaRules");

            _formulaRules = formulaRules;

            Text = "Formula Details";
            Image = Resources.Image_Formula;
        }

        public override void OnInitialize()
        {
            base.OnInitialize();

            Guard.AgainstNull(Model, "Model");

            View.FormulaNameRules = _formulaRules.FormulaNameRules();

            View.NameValue = Model.Name;
            View.MaximumFormulaNameValue = Model.MaximumFormulaName;
            View.MinimumFormulaNameValue = Model.MinimumFormulaName;
        }
    }
}