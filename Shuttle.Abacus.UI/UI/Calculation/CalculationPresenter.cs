using Abacus.Data;
using Abacus.Localisation;
using Abacus.Validation;

namespace Abacus.UI
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

            View.CalculationNameValue = CalculationColumns.Name.MapFrom(Model.Row);
            View.TypeValue = CalculationColumns.Type.MapFrom(Model.Row);
            View.RequiredValue = CalculationColumns.Required.MapFrom(Model.Row);

            View.DisableForEditing();
        }
    }
}
