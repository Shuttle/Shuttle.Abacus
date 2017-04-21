using System.Windows.Forms;
using Abacus.Infrastructure;
using Abacus.Validation;

namespace Abacus.UI
{
    public class ComboBoxValidator : ControlValidator<ComboBox>, IControlValidator
    {
        public ComboBoxValidator(IValidationConfiguration validationConfiguration) : base(validationConfiguration)
        {
        }

        public IResult Validate(Control control, IRuleCollection<object> rules)
        {
            return rules.BrokenBy(control.Text).ToResult();
        }

        public void WireValidationRequired(Control control, IControlValidation validation)
        {
            var combobox = (ComboBox) control;

            combobox.TextChanged +=
                delegate
                    {
                        combobox.BackColor = !validation.Evaluate().OK
                                                 ? ValidationConfiguration.ErrorColor
                                                 : validation.OriginalControlColor;
                    };
        }
    }
}
