using System;
using System.Windows.Forms;

namespace Abacus.UI
{
    public abstract class ControlValidator<T> where T : Control
    {
        private readonly Type type = typeof(T);

        public IValidationConfiguration ValidationConfiguration { get; private set; }

        protected ControlValidator(IValidationConfiguration validationConfiguration)
        {
            ValidationConfiguration = validationConfiguration;
        }

        public Type HandlesType
        {
            get { return type; }
        }

    }
}
