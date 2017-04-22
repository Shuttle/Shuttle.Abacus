using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.Invariants.Interfaces;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Calculation
{
    public class CalculationPresenter : Presenter<ICalculationView>, ICalculationPresenter
    {
        private readonly ICalculationRules calculationRules;

        public CalculationPresenter(ICalculationView view, ICalculationRules calculationRules)
            : base(view)
        {
            this.calculationRules = calculationRules;

            Text = "Calculation Details";

            Image = Resources.Image_Calculation;
        }

        public void CalculationNameExited()
        {
            WorkItem.Text = string.Format("Calculation{0}",
                                          View.CalculationNameValue.Length > 0
                                              ? " : " + View.CalculationNameValue
                                              : string.Empty);
        }

        public void TypeChanged()
        {
            if (View.TypeValue.ToLower() == "formula")
            {
                View.EnableFormulaInputs();
            }
            else
            {
                View.DisableFormulaInputs();
            }
        }

        public override void OnInitialize()
        {
            base.OnInitialize();

            View.CalculationNameRules = calculationRules.CalculationNameRules();
            View.TypeRules = calculationRules.TypeRules();

            if (Model == null)
            {
                return;
            }

            View.CalculationNameValue = CalculationColumns.Name.MapFrom(Model.GetRow());
            View.TypeValue = CalculationColumns.Type.MapFrom(Model.GetRow());
            View.RequiredValue = CalculationColumns.Required.MapFrom(Model.GetRow());

            View.DisableForEditing();
        }
    }
}
