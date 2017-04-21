using System;
using System.Drawing;
using System.Windows.Forms;
using Abacus.Infrastructure;
using Abacus.Validation;

namespace Abacus.UI
{
    public class ControlValidation : IControlValidation
    {
        private readonly IViewValidationManager manager;
        private readonly IControlValidator validator;
        private readonly IRuleCollection<object> rules;

        public ControlValidation(IViewValidationManager manager, IControlValidator validator, Control control, IRuleCollection<object> rules)
        {
            this.manager = manager;
            this.validator = validator;
            this.rules = rules;

            Control = control;
            OriginalControlColor = control.BackColor;

            validator.WireValidationRequired(control, this);
        }

        public IResult Evaluate()
        {
            try
            {
                if (!Control.Enabled)
                {
                    manager.ClearError(Control);

                    return Result.Create();
                }

                var result = validator.Validate(Control, rules);

                if (result.OK)
                {
                    manager.ClearError(Control);
                }
                else
                {
                    manager.SetError(Control, result.ToString());
                }

                return result;
            }
            catch (Exception ex)
            {
                return Result.Create().AddException(ex);
            }
        }

        public Control Control { get; private set; }
        public Color OriginalControlColor { get; private set; }
    }
}
