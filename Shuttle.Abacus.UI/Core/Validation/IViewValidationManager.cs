using System.Windows.Forms;

namespace Shuttle.Abacus.Shell.Core.Validation
{
    public interface IViewValidationManager
    {
        void ClearError(Control control);
        void SetError(Control control, string message);
    }
}
