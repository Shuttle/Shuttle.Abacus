using System.Windows.Forms;

namespace Shuttle.Abacus.UI.Core.Validation
{
    public interface IViewValidationManager
    {
        void ClearError(Control control);
        void SetError(Control control, string message);
    }
}
