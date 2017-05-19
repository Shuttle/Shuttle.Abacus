using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Shuttle.Abacus.Invariants.Core;

namespace Shuttle.Abacus.Shell.Core.Validation
{
    public class ViewValidator : IViewValidator, IControlValidatorBuild
    {
        private readonly IViewValidationManager manager;
        private readonly IControlValidatorProvider controlValidatorProvider;

        private readonly List<IControlValidation> validations = new List<IControlValidation>();

        private Control builderControl;

        public ViewValidator(IControlValidatorProvider controlValidatorProvider, IViewValidationManager manager)
        {
            this.controlValidatorProvider = controlValidatorProvider;
            this.manager = manager;
        }

        public void ValidateView()
        {
            IsValid = true;

            foreach (var validation in validations)
            {
                var validationResult = validation.Evaluate();

                if (validationResult.OK)
                {
                    manager.ClearError(validation.Control);
                }
                else
                {
                    manager.SetError(validation.Control, validationResult.ToString());

                    IsValid = false;
                }
            }
        }

        public bool IsValid { get; private set; }

        public IViewValidator ShouldSatisfy(IRule<object> rule)
        {
            return ShouldSatisfy(new RuleCollection<object>(rule));
        }

        private IControlValidator GetValidator()
        {
            var validator = controlValidatorProvider.GetFor(builderControl.GetType());

            if (validator == null)
            {
                throw new NotSupportedException(string.Format(Localisation.Resources.ControlValidatorNotFound,
                                                              builderControl.GetType().FullName));
            }
            return validator;
        }

        public IViewValidator ShouldSatisfy(IRuleCollection<object> rules)
        {
            var validator = GetValidator();

            validations.Add(new ControlValidation(manager, validator, builderControl, rules));

            return this;
        }

        public IControlValidatorBuild Control(Control control)
        {
            builderControl = control;

            return this;
        }
    }
}
