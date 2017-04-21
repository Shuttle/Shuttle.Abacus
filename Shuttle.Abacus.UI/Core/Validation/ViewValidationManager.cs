using System;
using System.Windows.Forms;

namespace Abacus.UI
{
    public class ViewValidationManager : IViewValidationManager
    {
        private ErrorProvider provider;

        public ViewValidationManager(ErrorProvider provider)
        {
            this.provider = provider;
        }

        public void ClearError(Control control)
        {
            provider.SetError(control, string.Empty);
        }

        public void SetError(Control control, string message)
        {
            provider.SetError(control, message);
        }
    }
}
