using Shuttle.Abacus.Invariants.Values;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.UI.Formula
{
    public class FormulaPresenter : Presenter<IFormulaView, FormulaModel>, IFormulaPresenter
    {
        private readonly IValueTypeValidatorProvider valueTypeValidatorProvider;

        public FormulaPresenter(IFormulaView view, IValueTypeValidatorProvider valueTypeValidatorProvider)
            : base(view)
        {
            this.valueTypeValidatorProvider = valueTypeValidatorProvider;
            Text = "Formula Details";
            Image = Resources.Image_Formula;
        }

        public override void OnInitialize()
        {
            base.OnInitialize();

            if (Model == null)
            {
                throw new NullDependencyException("Model");
            }

            if (Model.FormulaOperations == null)
            {
                return;
            }

            foreach (var formulaOperation in Model.FormulaOperations)
            {
            }
        }
    }
}