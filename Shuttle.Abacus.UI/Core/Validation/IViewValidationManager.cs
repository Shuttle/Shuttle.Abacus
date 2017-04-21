using System.Windows.Forms;

namespace Abacus.UI
{
    public interface IViewValidationManager
    {
        void ClearError(Control control);
        void SetError(Control control, string message);
    }
}
