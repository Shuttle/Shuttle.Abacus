using System;
using System.Windows.Forms;

namespace Shuttle.Abacus.UI.Core.Validation
{
    public abstract class ControlValidator<T> where T : Control
    {
        private readonly Type type = typeof(T);

        public IValidationConfiguration ValidationConfiguration { get; private set; }

        protected ControlValidator(IValidationConfiguration validationConfiguration)
        {
            ValidationConfiguration = validationConfiguration;
        }

        public Type HandlesType => type;
    }
}
