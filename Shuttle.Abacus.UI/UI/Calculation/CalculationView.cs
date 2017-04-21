using System;
using Abacus.Validation;

namespace Abacus.UI
{
    public partial class CalculationView : GenericCalculationView, ICalculationView
    {
        public CalculationView()
        {
            InitializeComponent();
        }

        public string CalculationNameValue
        {
            get { return CalculationName.Text; }
            set { CalculationName.Text = value; }
        }

        public string TypeValue
        {
            get { return Type.Text; }
            set { Type.Text = value; }
        }

        public bool RequiredValue
        {
            get { return Required.Checked; }
            set { Required.Checked = value; }
        }

        public IRuleCollection<object> CalculationNameRules
        {
            set { ViewValidator.Control(CalculationName).ShouldSatisfy(value); }
        }

        public IRuleCollection<object> TypeRules
        {
            set { ViewValidator.Control(Type).ShouldSatisfy(value); }
        }

        public void DisableForEditing()
        {
            Type.Enabled = false;
        }

        public void EnableFormulaInputs()
        {
            Required.Enabled = true;
        }

        public void DisableFormulaInputs()
        {
            Required.Checked = false;
            Required.Enabled = false;
        }

        private void CalculationName_Leave(object sender, EventArgs e)
        {
            Presenter.CalculationNameExited();
        }

        private void Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            Presenter.TypeChanged();
        }
    }

    public class GenericCalculationView : View<ICalculationPresenter>
    {
    }
}
